//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Text.Json.Serialization;
    public sealed class Core2Metadata
    {
        [JsonPropertyName(AttributeNames.ResourceType), JsonPropertyOrder(0)]
        public string ResourceType
        {
            get;
            set;
        }
        [JsonPropertyName(AttributeNames.Created), JsonPropertyOrder(1)]
        public DateTime Created
        {
            get;
            set;
        }
        [JsonPropertyName(AttributeNames.LastModified), JsonPropertyOrder(2)]
        public DateTime LastModified
        {
            get;
            set;
        }
        [JsonPropertyName(AttributeNames.Version), JsonPropertyOrder(3)]
        public string Version
        {
            get;
            set;
        }
        [JsonPropertyName(AttributeNames.Location), JsonPropertyOrder(4)]
        public string Location
        {
            get;
            set;
        }
    }
}
