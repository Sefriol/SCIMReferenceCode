// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

namespace Microsoft.SCIM
{
    public interface IPatch
    {
        Schematized PatchRequest { get; set; }
        IResourceIdentifier ResourceIdentifier { get; set; }
    }
}
