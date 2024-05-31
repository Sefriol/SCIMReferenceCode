//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Text.Json.Serialization;

    public sealed class Address : TypedItem
    {
        public const string Home = "home";
        public const string Other = "other";
        public const string Untyped = "untyped";
        public const string Work = "work";

        public Address()
        {
        }

        [JsonPropertyName(AttributeNames.Country)]
        public string Country
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Formatted)]
        public string Formatted
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Locality)]
        public string Locality
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.PostalCode)]
        public string PostalCode
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Region)]
        public string Region
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.StreetAddress)]
        public string StreetAddress
        {
            get;
            set;
        }
    }
}
