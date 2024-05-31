//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;

    public class QueryResponse : Schematized
    {
        [JsonPropertyName(ProtocolAttributeNames.Resources), JsonInclude, JsonPropertyOrder(3)]
        private Resource[] resources = null;

        public QueryResponse()
        {
            this.AddSchema(ProtocolSchemaIdentifiers.Version2ListResponse);
        }

        public QueryResponse(IReadOnlyCollection<Resource> resources)
            : this()
        {
            ArgumentNullException.ThrowIfNull(resources);

            this.resources = resources.ToArray();
        }

        public QueryResponse(IList<Resource> resources)
            : this()
        {
            ArgumentNullException.ThrowIfNull(resources);

            this.resources = resources.ToArray();
        }

        [JsonPropertyName(ProtocolAttributeNames.ItemsPerPage), JsonPropertyOrder(1)]
        public int ItemsPerPage { get; set; }

        [JsonIgnore]
        public IEnumerable<Resource> Resources
        {
            get { return this.resources; }

            set
            {
                if (null == value)
                {
                    throw new InvalidOperationException(SystemForCrossDomainIdentityManagementProtocolResources
                        .ExceptionInvalidValue);
                }

                this.resources = value.ToArray();
            }
        }

        [JsonPropertyName(ProtocolAttributeNames.StartIndex), JsonPropertyOrder(2)]
        public int? StartIndex { get; set; }

        [JsonPropertyName(ProtocolAttributeNames.TotalResults), JsonPropertyOrder(0)]
        public int TotalResults { get; set; }
    }

    public sealed class QueryResponse<TResource> : Schematized where TResource : Schematized
    {
        [JsonPropertyName(ProtocolAttributeNames.Resources), JsonInclude, JsonPropertyOrder(3)]
        private TResource[] resources;

        public QueryResponse(string schemaIdentifier)
        {
            if (string.IsNullOrWhiteSpace(schemaIdentifier))
            {
                throw new ArgumentNullException(nameof(schemaIdentifier));
            }

            this.AddSchema(schemaIdentifier);
            this.OnInitialization();
        }

        public QueryResponse(string schemaIdentifier, IReadOnlyCollection<TResource> resources)
            : this(schemaIdentifier)
        {
            ArgumentNullException.ThrowIfNull(resources);

            this.resources = resources.ToArray();
        }

        public QueryResponse(string schemaIdentifier, IList<TResource> resources)
            : this(schemaIdentifier)
        {
            ArgumentNullException.ThrowIfNull(resources);

            this.resources = resources.ToArray();
        }

        [JsonPropertyName(ProtocolAttributeNames.ItemsPerPage), JsonPropertyOrder(1)]
        public int ItemsPerPage { get; set; }

        [JsonIgnore]
        public IEnumerable<TResource> Resources
        {
            get { return this.resources; }

            set
            {
                if (null == value)
                {
                    throw new InvalidOperationException(SystemForCrossDomainIdentityManagementProtocolResources
                        .ExceptionInvalidValue);
                }

                this.resources = value.ToArray();
            }
        }

        [JsonPropertyName(ProtocolAttributeNames.StartIndex), JsonPropertyOrder(2)]
        public int? StartIndex { get; set; }

        [JsonPropertyName(ProtocolAttributeNames.TotalResults), JsonPropertyOrder(0)]
        public int TotalResults { get; set; }

        public new void OnDeserializing()
        {
            this.OnInitialization();
        }

        private void OnInitialization()
        {
            this.resources = Array.Empty<TResource>();
        }
    }
}
