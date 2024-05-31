// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

namespace Microsoft.SCIM
{
    using System;

    public sealed class Patch : IPatch
    {
        public Patch()
        {
        }

        public Patch(IResourceIdentifier resourceIdentifier, Schematized request)
        {
            this.ResourceIdentifier = resourceIdentifier ?? throw new ArgumentNullException(nameof(resourceIdentifier));
            this.PatchRequest = request ?? throw new ArgumentNullException(nameof(request));
        }

        public Schematized PatchRequest
        {
            get;
            set;
        }

        public IResourceIdentifier ResourceIdentifier
        {
            get;
            set;
        }
    }
}
