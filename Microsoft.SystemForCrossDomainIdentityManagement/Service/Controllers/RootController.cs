// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

namespace Microsoft.SCIM
{
    using System;

    public sealed class RootController : ControllerTemplate<Resource, IProvider<Resource>>
    {
        public RootController(IProvider<Resource> provider, IMonitor monitor)
            : base(provider, monitor)
        {
        }

        protected override IProviderAdapter<Resource> AdaptProvider(IProvider<Resource> provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            IProviderAdapter<Resource> result = new RootProviderAdapter(provider);
            return result;
        }
    }
}
