// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

namespace Microsoft.SCIM
{
    internal class Core2EnterpriseUserProviderAdapter : ProviderAdapterTemplate<Core2EnterpriseUser>
    {
        public Core2EnterpriseUserProviderAdapter(IProvider<Core2EnterpriseUser> provider)
            : base(provider)
        {
        }

        public override string SchemaIdentifier
        {
            get
            {
                return SchemaIdentifiers.Core2EnterpriseUser;
            }
        }
    }
}
