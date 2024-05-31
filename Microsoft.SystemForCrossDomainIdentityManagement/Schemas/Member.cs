//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Text.Json.Serialization;
    public sealed class Member
    {
        public Member()
        {
        }

        [JsonPropertyName(AttributeNames.Type), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string TypeName
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Value)]
        public string Value
        {
            get;
            set;
        }
    }
}
