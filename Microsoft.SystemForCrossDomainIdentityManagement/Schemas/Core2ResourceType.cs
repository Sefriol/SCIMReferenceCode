//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Text.Json.Serialization;

    public sealed class Core2ResourceType : Resource, IJsonOnDeserialized
    {
        private Uri endpoint;

        [JsonPropertyName(AttributeNames.Endpoint)]
        public string endpointValue;

        [JsonPropertyName(AttributeNames.Name), JsonInclude]
        private string name;

        public Core2ResourceType()
        {
            this.AddSchema(SchemaIdentifiers.Core2ResourceType);
            this.Metadata =
                new Core2Metadata()
                {
                    ResourceType = Types.ResourceType
                };
        }

        public Uri Endpoint
        {
            get { return this.endpoint; }

            set
            {
                this.endpoint = value;
                this.endpointValue = new SystemForCrossDomainIdentityManagementResourceIdentifier(value).RelativePath;
            }
        }

        [JsonPropertyName(AttributeNames.Metadata)]
        public Core2Metadata Metadata { get; set; }

        [JsonPropertyName(AttributeNames.Schema)]
        public string Schema { get; set; }

        private void InitializeEndpoint(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                this.endpoint = null;
                return;
            }

            this.endpoint = new Uri(value, UriKind.Relative);
        }

        private void InitializeEndpoint()
        {
            this.InitializeEndpoint(this.endpointValue);
        }

        public new void OnDeserialized()
        {
            this.InitializeEndpoint();
        }

        public new void OnSerializing()
        {
            this.name = this.Identifier;
        }
    }
}
