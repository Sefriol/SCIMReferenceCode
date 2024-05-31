//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Text.Json.Serialization;
    public sealed class BulkRequest2 : BulkOperations<BulkRequestOperation>
    {
        public BulkRequest2()
            : base(ProtocolSchemaIdentifiers.Version2BulkRequest)
        {
        }

        [JsonPropertyName(ProtocolAttributeNames.FailOnErrors), JsonPropertyOrder(1)]
        public int? FailOnErrors
        {
            get;
            set;
        }
    }
}
