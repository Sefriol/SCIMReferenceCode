//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Text.Json.Serialization;
    public sealed class Name
    {
        [JsonPropertyName(AttributeNames.Formatted), JsonPropertyOrder(0), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Formatted
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.FamilyName), JsonPropertyOrder(1), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string FamilyName
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.GivenName), JsonPropertyOrder(1), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string GivenName
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.HonorificPrefix), JsonPropertyOrder(1), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string HonorificPrefix
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.HonorificSuffix), JsonPropertyOrder(1), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string HonorificSuffix
        {
            get;
            set;
        }
    }
}
