//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Text.Json.Serialization;

    public sealed class BulkRequestsFeature : FeatureBase
    {
        public BulkRequestsFeature()
        {
        }

        public int ConcurrentOperations
        {
            get;
            private set;
        }

        [JsonPropertyName(AttributeNames.MaximumOperations)]
        public int MaximumOperations
        {
            get;
            private set;
        }

        [JsonPropertyName(AttributeNames.MaximumPayloadSize)]
        public int MaximumPayloadSize
        {
            get;
            private set;
        }

        public static BulkRequestsFeature CreateUnsupportedFeature()
        {
            BulkRequestsFeature result =
                new BulkRequestsFeature()
                {
                    Supported = false
                };
            return result;
        }
    }
}
