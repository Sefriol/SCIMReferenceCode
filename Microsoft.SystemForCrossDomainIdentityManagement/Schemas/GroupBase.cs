//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    public abstract class GroupBase : Resource
    {
        [JsonPropertyName(AttributeNames.DisplayName)]
        public virtual string DisplayName
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Members), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual IEnumerable<Member> Members
        {
            get;
            set;
        }
    }
}
