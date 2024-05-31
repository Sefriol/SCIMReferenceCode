//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Text.Json.Serialization;

    public sealed class Core2EnterpriseUser : Core2User
    {
        public Core2EnterpriseUser()
            : base()
        {
            this.AddSchema(SchemaIdentifiers.Core2EnterpriseUser);
            this.EnterpriseExtension = new ExtensionAttributeEnterpriseUser2();
        }

        [JsonPropertyName(AttributeNames.ExtensionEnterpriseUser2)]
        public ExtensionAttributeEnterpriseUser2 EnterpriseExtension
        {
            get;
            set;
        }
    }
}
