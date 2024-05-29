//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class Core2Error : Schematized
    {
        public Core2Error(
            string detail,
            int status,
            string scimType = null // https://datatracker.ietf.org/doc/html/rfc7644#section-3.12
        )
        {
            this.AddSchema(ProtocolSchemaIdentifiers.Version2Error);

            this.Detail = detail;
            this.Status = status;
            this.ScimType = scimType != null ? scimType : null;
        }

        [DataMember(Name = "scimType", Order = 1)] //AttributeNames.ScimType
        public string ScimType { get; set; }

        [DataMember(Name = "detail", Order = 2)] //AttributeNames.Detail
        public string Detail { get; set; }

        [DataMember(Name = "status", Order = 3)] //AttributeNames.Status
        public int Status { get; set; }
    }
}
