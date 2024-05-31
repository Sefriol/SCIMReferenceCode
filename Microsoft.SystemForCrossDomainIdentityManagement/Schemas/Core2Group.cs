//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text.Json.Serialization;

    public class Core2Group : GroupBase, IJsonOnDeserializing
    {
        private IDictionary<string, IDictionary<string, object>> customExtension;

        public Core2Group()
        {
            this.AddSchema(SchemaIdentifiers.Core2Group);
            this.Metadata =
                new Core2Metadata()
                {
                    ResourceType = Types.Group
                };
            this.OnInitialization();
        }

        public virtual IReadOnlyDictionary<string, IDictionary<string, object>> CustomExtension
        {
            get
            {
                return new ReadOnlyDictionary<string, IDictionary<string, object>>(this.customExtension);
            }
        }

        [JsonPropertyName(AttributeNames.Metadata)]
        public Core2Metadata Metadata
        {
            get;
            set;
        }

        public virtual void AddCustomAttribute(string key, object value)
        {
            if
            (
                    key != null
                && key.StartsWith(SchemaIdentifiers.PrefixExtension, StringComparison.OrdinalIgnoreCase)
                && value is Dictionary<string, object> nestedObject
            )
            {
                this.customExtension.Add(key, nestedObject);
            }
        }

        public new void OnDeserializing()
        {
            this.OnInitialization();
        }

        private void OnInitialization()
        {
            this.customExtension = new Dictionary<string, IDictionary<string, object>>();
        }

        public override Dictionary<string, object> ToJson()
        {
            Dictionary<string, object> result = base.ToJson();

            foreach (KeyValuePair<string, IDictionary<string, object>> entry in this.CustomExtension)
            {
                result.Add(entry.Key, entry.Value);
            }

            return result;
        }
    }
}
