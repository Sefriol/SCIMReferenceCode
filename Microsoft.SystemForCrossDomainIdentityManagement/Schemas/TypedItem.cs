//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Text.Json.Serialization;
    public abstract class TypedItem
    {
        [JsonPropertyName(AttributeNames.Type)]
        public string ItemType
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Primary)]
        public bool Primary
        {
            get;
            set;
        }
    }
}
