//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Text.Json.Serialization;
    public sealed class Role : TypedItem
    {
        [JsonPropertyName(AttributeNames.Display), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Display
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Value), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Value
        {
            get;
            set;
        }
    }
}
