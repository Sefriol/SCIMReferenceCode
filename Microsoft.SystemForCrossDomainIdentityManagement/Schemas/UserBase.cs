//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Text.Json.Serialization;
    public abstract class UserBase : Resource
    {
        [JsonPropertyName(AttributeNames.UserName)]
        public virtual string UserName
        {
            get;
            set;
        }
    }
}
