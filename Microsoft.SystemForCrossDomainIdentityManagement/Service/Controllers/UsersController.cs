// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

namespace Microsoft.SCIM
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route(ServiceConstants.RouteUsers)]
    [Authorize]
    [ApiController]
    public sealed class UsersController : ControllerTemplate<Core2EnterpriseUser, IProvider<Core2EnterpriseUser>>
    {
        public UsersController(IProvider<Core2EnterpriseUser> provider, IMonitor monitor)
            : base(provider, monitor)
        {
        }

        protected override IProviderAdapter<Core2EnterpriseUser> AdaptProvider(IProvider<Core2EnterpriseUser> provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            IProviderAdapter<Core2EnterpriseUser> result = new Core2EnterpriseUserProviderAdapter(provider);
            return result;
        }
    }
}
