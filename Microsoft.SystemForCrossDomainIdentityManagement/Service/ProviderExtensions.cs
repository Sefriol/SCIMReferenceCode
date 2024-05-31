//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Collections.Generic;

    public static class ProviderExtension
    {
        public static IReadOnlyCollection<IExtension> ReadExtensions<T>(this IProvider<T> provider) where T : Schematized
        {
            ArgumentNullException.ThrowIfNull(provider);

            IReadOnlyCollection<IExtension> result;
            try
            {
                result = provider.Extensions;
            }
            catch (NotImplementedException)
            {
                result = null;
            }

            return result;
        }
    }

}
