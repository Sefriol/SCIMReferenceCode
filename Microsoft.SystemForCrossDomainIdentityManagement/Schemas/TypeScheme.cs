//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;

    public sealed class TypeScheme : Resource, IJsonOnDeserialized, IJsonOnDeserializing
    {
        private List<AttributeScheme> attributes;
        private IReadOnlyCollection<AttributeScheme> attributesWrapper;

        private object thisLock;

        public TypeScheme()
        {
            this.OnInitialization();
            this.OnInitialized();
            this.AddSchema(SchemaIdentifiers.Core2Schema);
            this.Metadata =
                new Core2Metadata()
                {
                    ResourceType = Types.Schema
                };
        }

        [JsonPropertyName(AttributeNames.Attributes), JsonPropertyOrder(0)]
        public IReadOnlyCollection<AttributeScheme> Attributes => this.attributesWrapper;

        [JsonPropertyName(AttributeNames.Name), JsonInclude]
        public string Name
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Description)]
        public string Description
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Metadata)]
        public Core2Metadata Metadata
        {
            get;
            set;
        }

        public void AddAttribute(AttributeScheme attribute)
        {
            ArgumentNullException.ThrowIfNull(attribute);

            Func<bool> containsFunction =
                new Func<bool>(
                        () =>
                            this
                            .attributes
                            .Any(
                                (AttributeScheme item) =>
                                    string.Equals(item.Name, attribute.Name, StringComparison.OrdinalIgnoreCase)));


            if (!containsFunction())
            {
                lock (this.thisLock)
                {
                    if (!containsFunction())
                    {
                        this.attributes.Add(attribute);
                    }
                }
            }
        }

        public new void OnDeserialized()
        {
            this.OnInitialized();
        }

        public new void OnDeserializing()
        {
            this.OnInitialization();
        }

        private void OnInitialization()
        {
            this.thisLock = new object();
            this.attributes = new List<AttributeScheme>();
        }

        private void OnInitialized()
        {
            this.attributesWrapper = this.attributes.AsReadOnly();
        }
    }
}
