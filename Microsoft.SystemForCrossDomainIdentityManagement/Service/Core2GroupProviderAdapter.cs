// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

namespace Microsoft.SCIM
{
    internal class Core2GroupProviderAdapter : ProviderAdapterTemplate<Core2Group>
    {
        public Core2GroupProviderAdapter(IProvider<Core2Group> provider)
            : base(provider)
        {
        }

        public override string SchemaIdentifier
        {
            get
            {
                return SchemaIdentifiers.Core2Group;
            }
        }
    }
}
