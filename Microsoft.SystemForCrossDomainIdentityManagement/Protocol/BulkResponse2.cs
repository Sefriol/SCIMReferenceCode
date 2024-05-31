//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    public sealed class BulkResponse2 : BulkOperations<BulkResponseOperation>
    {
        public BulkResponse2()
            : base(ProtocolSchemaIdentifiers.Version2BulkResponse)
        {
        }
    }
}
