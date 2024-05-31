//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Text.Json.Serialization;
    public sealed class Manager
    {
        [JsonPropertyName(AttributeNames.Value)]
        public string Value
        {
            get;
            set;
        }
    }
}
