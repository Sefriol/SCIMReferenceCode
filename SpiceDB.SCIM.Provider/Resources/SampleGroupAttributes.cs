﻿// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.


namespace SpiceDB.SCIM.Provider.Resources
{
    using Microsoft.SCIM;

    public static class SampleGroupAttributes
    {
        /*public static AttributeScheme GroupDisplayNameAttributeScheme
        {
            get
            {
                AttributeScheme groupDisplayScheme =
                    new AttributeScheme("displayName", AttributeDataType.@string, false)
                    {
                        Description = SampleConstants.DescriptionGroupDisplayName,
                        Required = true,
                        Uniqueness = Uniqueness.server
                    };

                return groupDisplayScheme;
            }
        }*/

        public static AttributeScheme MembersAttributeScheme
        {
            get
            {
                AttributeScheme membersScheme = new AttributeScheme("members", AttributeDataType.complex, true)
                {
                    Description = SampleConstants.DescriptionMemebers
                };
                membersScheme.AddSubAttribute(SampleMultivaluedAttributes.ValueSubAttributeScheme);
                membersScheme.AddSubAttribute(SampleMultivaluedAttributes.TypeSubAttributeScheme);

                return membersScheme;
            }
        }
    }
}
