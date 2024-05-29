// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

using Microsoft.SCIM;

namespace SpiceDB.SCIM.Provider.Resources
{
    public static class SampleTypeScheme
    {

        /*public static TypeScheme UserTypeScheme
        {
            get
            {
                TypeScheme userType = new TypeScheme
                {
                    Description = SampleConstants.UserAccount,
                    Identifier = $"{SampleConstants.Core2SchemaPrefix}{Types.User}",
                    Name = Types.User
                };
                userType.AddAttribute(SampleUserAttributes.UserNameAttributeScheme);
                userType.AddAttribute(SampleUserAttributes.DisplayNameAttributeScheme);
                userType.AddAttribute(SampleUserAttributes.UserTypeAttributeScheme);
                userType.AddAttribute(SampleUserAttributes.RolesAttributeScheme);

                return userType;
            }
        }

        public static TypeScheme EnterpriseUserTypeScheme
        {
            get
            {
                TypeScheme enterpriseType = new TypeScheme
                {
                    Description = SampleConstants.UserEnterprise,
                    Identifier = SampleConstants.UserEnterpriseSchema,
                    Name = SampleConstants.UserEnterpriseName
                };

                return enterpriseType;
            }
        }*/

        public static TypeScheme GroupTypeScheme
        {
            get
            {
                TypeScheme groupType = new TypeScheme
                {
                    Description = Types.Group,
                    Identifier = $"{SampleConstants.Core2SchemaPrefix}{Types.Group}",
                    Name = Types.Group
                };
                groupType.AddAttribute(SampleGroupAttributes.MembersAttributeScheme);
                return groupType;
            }
        }

        public static TypeScheme ResourceTypesTypeScheme
        {
            get
            {
                TypeScheme resourceTypesType = new TypeScheme
                {
                    Description = SampleConstants.DescriptionResourceTypeSchema,
                    Identifier = $"{SampleConstants.Core2SchemaPrefix}{Types.ResourceType}",
                    Name = Types.ResourceType
                };
                resourceTypesType.AddAttribute(SampleCommonAttributes.IdentifierAttributeScheme);
                resourceTypesType.AddAttribute(SampleResourceTypeAttributes.NameAttributeScheme);
                resourceTypesType.AddAttribute(SampleResourceTypeAttributes.EndpointAttributeScheme);
                resourceTypesType.AddAttribute(SampleResourceTypeAttributes.SchemaAttributeScheme);

                return resourceTypesType;
            }
        }

        public static TypeScheme SchemaTypeScheme
        {
            get
            {
                TypeScheme schemaType = new TypeScheme
                {
                    Description = SampleConstants.DescriptionScimSchema,
                    Identifier = $"{SampleConstants.Core2SchemaPrefix}{Types.Schema}",
                    Name = Types.Schema
                };
                schemaType.AddAttribute(SampleCommonAttributes.IdentifierAttributeScheme);
                schemaType.AddAttribute(SampleSchemaAttributes.NameAttributeScheme);
                schemaType.AddAttribute(SampleSchemaAttributes.DescriptionAttributeScheme);
                schemaType.AddAttribute(SampleSchemaAttributes.AttributesAttributeScheme);

                return schemaType;
            }
        }

        public static TypeScheme ServiceProviderConfigTypeScheme
        {
            get
            {
                TypeScheme serviceProviderConfigType = new TypeScheme
                {
                    Description = SampleConstants.DescriptionServiceProviderConfigSchema,
                    Identifier = $"{SampleConstants.Core2SchemaPrefix}{Types.ServiceProviderConfiguration}",
                    Name = SampleConstants.ServiceProviderConfig
                };
                serviceProviderConfigType.AddAttribute(SampleServiceProviderConfigAttributes.DocumentationUriAttributeScheme);
                serviceProviderConfigType.AddAttribute(SampleServiceProviderConfigAttributes.PatchAttributeScheme);
                serviceProviderConfigType.AddAttribute(SampleServiceProviderConfigAttributes.BulkAttributeScheme);
                serviceProviderConfigType.AddAttribute(SampleServiceProviderConfigAttributes.FilterAttributeScheme);
                serviceProviderConfigType.AddAttribute(SampleServiceProviderConfigAttributes.ChangePasswordAttributeScheme);
                serviceProviderConfigType.AddAttribute(SampleServiceProviderConfigAttributes.SortAttributeScheme);
                serviceProviderConfigType.AddAttribute(SampleServiceProviderConfigAttributes.EtagAttributeScheme);
                serviceProviderConfigType.AddAttribute(SampleServiceProviderConfigAttributes.AuthenticationSchemesAttributeScheme);

                return serviceProviderConfigType;
            }
        }
    }
}
