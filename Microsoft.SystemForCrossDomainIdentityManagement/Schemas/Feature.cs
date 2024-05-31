//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    public sealed class Feature : FeatureBase
    {
        public Feature(bool supported)
        {
            this.Supported = supported;
        }
    }
}
