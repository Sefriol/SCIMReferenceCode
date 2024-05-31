//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Text.Json.Serialization;
    public abstract class FeatureBase
    {
        [JsonPropertyName(AttributeNames.Supported)]
        public bool Supported
        {
            get;
            set;
        }
    }
}
