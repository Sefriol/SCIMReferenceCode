// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

namespace Microsoft.SCIM
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route(ServiceConstants.RouteGroups)]
    [Authorize]
    [ApiController]
    public sealed class GroupsController : ControllerTemplate<Core2Group, IProvider<Core2Group>>
    {
        public GroupsController(IProvider<Core2Group> provider, IMonitor monitor)
            : base(provider, monitor)
        {
        }

        protected override IProviderAdapter<Core2Group> AdaptProvider(IProvider<Core2Group> provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            IProviderAdapter<Core2Group> result =
                new Core2GroupProviderAdapter(provider);
            return result;
        }
    }
}
