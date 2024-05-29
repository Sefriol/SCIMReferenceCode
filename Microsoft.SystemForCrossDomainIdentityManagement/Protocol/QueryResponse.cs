//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class QueryResponse : Schematized
    {
        [DataMember(Name = ProtocolAttributeNames.Resources, Order = 3)]
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

        [DataMember(Name = ProtocolAttributeNames.ItemsPerPage, Order = 1)]
        public int ItemsPerPage
        {
            get;
            set;
        }

        public IEnumerable<Resource> Resources
        {
            get
            {
                return this.resources;
            }

            set
            {
                if (null == value)
                {
                    throw new InvalidOperationException(SystemForCrossDomainIdentityManagementProtocolResources.ExceptionInvalidValue);
                }

                this.resources = value.ToArray();
            }
        }

        [DataMember(Name = ProtocolAttributeNames.StartIndex, Order = 2)]
        public int? StartIndex
        {
            get;
            set;
        }

        [DataMember(Name = ProtocolAttributeNames.TotalResults, Order = 0)]
        public int TotalResults
        {
            get;
            set;
        }
    }

    [DataContract]
    public sealed class QueryResponse<TResource> : Schematized where TResource : Resource
    {
        [DataMember(Name = ProtocolAttributeNames.Resources, Order = 3)]
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

        [DataMember(Name = ProtocolAttributeNames.ItemsPerPage, Order = 1)]
        public int ItemsPerPage
        {
            get;
            set;
        }

        public IEnumerable<TResource> Resources
        {
            get
            {
                return this.resources;
            }

            set
            {
                if (null == value)
                {
                    throw new InvalidOperationException(SystemForCrossDomainIdentityManagementProtocolResources.ExceptionInvalidValue);
                }
                this.resources = value.ToArray();
            }
        }

        [DataMember(Name = ProtocolAttributeNames.StartIndex, Order = 2)]
        public int? StartIndex
        {
            get;
            set;
        }

        [DataMember(Name = ProtocolAttributeNames.TotalResults, Order = 0)]
        public int TotalResults
        {
            get;
            set;
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            this.OnInitialization();
        }

        private void OnInitialization()
        {
            this.resources = Array.Empty<TResource>();
        }
    }
}
