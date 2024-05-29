// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

/*using Microsoft.SCIM;

namespace SpiceDB.SCIM.Provider.Resources
{
    public static class SampleUserAttributes
    {
        public static AttributeScheme UserNameAttributeScheme
        {
            get
            {
                AttributeScheme userNameScheme = new AttributeScheme("userName", AttributeDataType.@string, false)
                {
                    Description = SampleConstants.DescriptionUserName,
                    Required = true,
                    Uniqueness = Uniqueness.server
                };
                return userNameScheme;
            }
        }

        public static AttributeScheme DisplayNameAttributeScheme
        {
            get
            {
                AttributeScheme displayNameScheme = new AttributeScheme("displayName", AttributeDataType.@string, false)
                {
                    Description = SampleConstants.DescriptionDisplayName
                };
                return displayNameScheme;
            }
        }

        public static AttributeScheme UserTypeAttributeScheme
        {
            get
            {
                AttributeScheme userTypeScheme = new AttributeScheme("userType", AttributeDataType.@string, false)
                {
                    Description = SampleConstants.DescriptionUserType
                };
                return userTypeScheme;
            }
        }

        public static AttributeScheme RolesAttributeScheme
        {
            get
            {
                AttributeScheme rolesScheme = new AttributeScheme("roles", AttributeDataType.complex, true)
                {
                    Description = SampleConstants.DescriptionRoles
                };
                rolesScheme.AddSubAttribute(SampleMultivaluedAttributes.ValueSubAttributeScheme);
                rolesScheme.AddSubAttribute(SampleMultivaluedAttributes.DisplaySubAttributeScheme);
                rolesScheme.AddSubAttribute(SampleMultivaluedAttributes.TypeDefaultSubAttributeScheme);
                rolesScheme.AddSubAttribute(SampleMultivaluedAttributes.PrimarySubAttributeScheme);

                return rolesScheme;
            }
        }
    }
}*/
