// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.


namespace SpiceDB.SCIM.Provider.Resources
{
    using Microsoft.SCIM;

    public static class SampleMultivaluedAttributes
    {
        public static AttributeScheme ValueSubAttributeScheme
        {
            get
            {
                AttributeScheme valueScheme = new AttributeScheme("value", AttributeDataType.@string, false)
                {
                    Description = SampleConstants.DescriptionValue,
                };
                return valueScheme;
            }
        }

        public static AttributeScheme TypeSubAttributeScheme
        {
            get
            {
                AttributeScheme typeScheme = new AttributeScheme("type", AttributeDataType.@string, false)
                {
                    Description = SampleConstants.DescriptionType,
                    Mutability = Mutability.immutable
                };
                typeScheme.AddCanonicalValues(Types.Group);
                typeScheme.AddCanonicalValues(Types.User);

                return typeScheme;
            }
        }

        /*public static AttributeScheme DisplaySubAttributeScheme
        {
            get
            {
                AttributeScheme typeScheme = new AttributeScheme("display", AttributeDataType.@string, false)
                {
                    Description = SampleConstants.DescriptionDisplay
                };
                return typeScheme;
            }
        }*/

        public static AttributeScheme TypeAuthenticationSchemesAttributeScheme
        {
            get
            {
                AttributeScheme typeScheme = new AttributeScheme("type", AttributeDataType.@string, false)
                {
                    Description = SampleConstants.DescriptionType,
                    Required = true
                };
                typeScheme.AddCanonicalValues("oauth");
                typeScheme.AddCanonicalValues("oauth2");
                typeScheme.AddCanonicalValues("oauthbearertoken");
                typeScheme.AddCanonicalValues("httpbasic");
                typeScheme.AddCanonicalValues("httpdigest");

                return typeScheme;
            }
        }

        /*public static AttributeScheme TypeDefaultSubAttributeScheme
        {
            get
            {
                AttributeScheme typeScheme = new AttributeScheme("type", AttributeDataType.@string, false)
                {
                    Description = SampleConstants.DescriptionType,
                };
                return typeScheme;
            }
        }

        public static AttributeScheme PrimarySubAttributeScheme
        {
            get
            {
                AttributeScheme typeScheme = new AttributeScheme("primary", AttributeDataType.boolean, false)
                {
                    Description = SampleConstants.DescriptionPrimary
                };
                return typeScheme;
            }
        }*/
    }
}
