﻿//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class Address : TypedItem
    {
        public const string Home = "home";
        public const string Other = "other";
        public const string Untyped = "untyped";
        public const string Work = "work";

        internal Address()
        {
        }

        [DataMember(Name = AttributeNames.Country, IsRequired = false, EmitDefaultValue = false)]
        public string Country
        {
            get;
            set;
        }

        [DataMember(Name = AttributeNames.Formatted, IsRequired = false, EmitDefaultValue = false)]
        public string Formatted
        {
            get;
            set;
        }

        [DataMember(Name = AttributeNames.Locality, IsRequired = false, EmitDefaultValue = false)]
        public string Locality
        {
            get;
            set;
        }

        [DataMember(Name = AttributeNames.PostalCode, IsRequired = false, EmitDefaultValue = false)]
        public string PostalCode
        {
            get;
            set;
        }

        [DataMember(Name = AttributeNames.Region, IsRequired = false, EmitDefaultValue = false)]
        public string Region
        {
            get;
            set;
        }

        [DataMember(Name = AttributeNames.StreetAddress, IsRequired = false, EmitDefaultValue = false)]
        public string StreetAddress
        {
            get;
            set;
        }
    }
}
