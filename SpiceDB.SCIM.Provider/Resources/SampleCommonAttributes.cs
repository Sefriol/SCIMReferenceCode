// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.


namespace SpiceDB.SCIM.Provider.Resources
{
    using Microsoft.SCIM;

    public static class SampleCommonAttributes
    {
        public static AttributeScheme IdentifierAttributeScheme
        {
            get
            {
                AttributeScheme idScheme = new AttributeScheme("id", AttributeDataType.@string, false)
                {
                    Description = SampleConstants.DescriptionIdentifier
                };
                return idScheme;
            }
        }
    }
}
