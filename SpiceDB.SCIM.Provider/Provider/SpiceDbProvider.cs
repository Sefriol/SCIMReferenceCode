namespace SpiceDB.SCIM.Provider.Provider
{
    using Authzed.Api.V1;
    using Microsoft.SCIM;

    public class SpiceDbProvider : ProviderBase
    {
        private readonly ProviderBase groupProvider;
        private readonly ProviderBase userProvider;

        private static readonly Lazy<IReadOnlyCollection<TypeScheme>> TypeSchema =
            new(
                () =>
                    new[]
                    {
                        //Resources.SampleTypeScheme.UserTypeScheme,
                        Resources.SampleTypeScheme.GroupTypeScheme,
                        //Resources.SampleTypeScheme.EnterpriseUserTypeScheme,
                        Resources.SampleTypeScheme.ResourceTypesTypeScheme,
                        Resources.SampleTypeScheme.SchemaTypeScheme,
                        Resources.SampleTypeScheme.ServiceProviderConfigTypeScheme
                    });

        private static readonly Lazy<IReadOnlyCollection<Core2ResourceType>> Types =
            new(
                () =>
                    new[]
                    {
                        Resources.SampleResourceTypes.UserResourceType, Resources.SampleResourceTypes.GroupResourceType
                    });


        public SpiceDbProvider(PermissionsService.PermissionsServiceClient permissionsServiceClient)
        {
            this.groupProvider = new SpiceDbGroupProvider(permissionsServiceClient);
            //this.userProvider = new InMemoryUserProvider();
        }

        public override IReadOnlyCollection<Core2ResourceType> ResourceTypes => SpiceDbProvider.Types.Value;

        public override IReadOnlyCollection<TypeScheme> Schema => SpiceDbProvider.TypeSchema.Value;

        public override Task<Resource> CreateAsync(Resource resource, string correlationIdentifier)
        {
            /*if (resource is Core2EnterpriseUser)
            {
                return this.userProvider.CreateAsync(resource, correlationIdentifier);
            }*/

            if (resource is Core2Group)
            {
                return this.groupProvider.CreateAsync(resource, correlationIdentifier);
            }

            throw new NotImplementedException();
        }

        public override Task<Resource> DeleteAsync(IResourceIdentifier resourceIdentifier, string correlationIdentifier)
        {
            if (resourceIdentifier.SchemaIdentifier.Equals(SchemaIdentifiers.Core2EnterpriseUser))
            {
                return this.userProvider.DeleteAsync(resourceIdentifier, correlationIdentifier);
            }

            if (resourceIdentifier.SchemaIdentifier.Equals(SchemaIdentifiers.Core2Group))
            {
                return this.groupProvider.DeleteAsync(resourceIdentifier, correlationIdentifier);
            }

            throw new NotImplementedException();
        }

        public override Task<Resource[]> QueryAsync(IQueryParameters parameters, string correlationIdentifier)
        {
            if (parameters.SchemaIdentifier.Equals(SchemaIdentifiers.Core2EnterpriseUser))
            {
                return this.userProvider.QueryAsync(parameters, correlationIdentifier);
            }

            if (parameters.SchemaIdentifier.Equals(SchemaIdentifiers.Core2Group))
            {
                return this.groupProvider.QueryAsync(parameters, correlationIdentifier);
            }

            throw new NotImplementedException();
        }

        public override Task<QueryResponseBase> PaginateQueryAsync(IRequest<IQueryParameters> request)
        {
            if (request.Payload.SchemaIdentifier.Equals(SchemaIdentifiers.Core2EnterpriseUser))
            {
                return this.userProvider.PaginateQueryAsync(request);
            }

            if (request.Payload.SchemaIdentifier.Equals(SchemaIdentifiers.Core2Group))
            {
                return this.groupProvider.PaginateQueryAsync(request);
            }

            throw new NotImplementedException();
        }

        public override Task<Resource> ReplaceAsync(Resource resource, string correlationIdentifier)
        {
            if (resource is Core2EnterpriseUser)
            {
                return this.userProvider.ReplaceAsync(resource, correlationIdentifier);
            }

            if (resource is Core2Group)
            {
                return this.groupProvider.ReplaceAsync(resource, correlationIdentifier);
            }

            throw new NotImplementedException();
        }

        public override Task<Resource> RetrieveAsync(IResourceRetrievalParameters parameters,
            string correlationIdentifier)
        {
            if (parameters.SchemaIdentifier.Equals(SchemaIdentifiers.Core2EnterpriseUser))
            {
                return this.userProvider.RetrieveAsync(parameters, correlationIdentifier);
            }

            if (parameters.SchemaIdentifier.Equals(SchemaIdentifiers.Core2Group))
            {
                return this.groupProvider.RetrieveAsync(parameters, correlationIdentifier);
            }

            throw new NotImplementedException();
        }

        public override Task<Resource> UpdateAsync(IPatch patch, string correlationIdentifier)
        {
            if (patch == null)
            {
                throw new ArgumentNullException(nameof(patch));
            }

            if (string.IsNullOrWhiteSpace(patch.ResourceIdentifier.Identifier))
            {
                throw new ArgumentException(nameof(patch));
            }

            if (string.IsNullOrWhiteSpace(patch.ResourceIdentifier.SchemaIdentifier))
            {
                throw new ArgumentException(nameof(patch));
            }

            /*if (patch.ResourceIdentifier.SchemaIdentifier.Equals(SchemaIdentifiers.Core2EnterpriseUser))
            {
                return this.userProvider.UpdateAsync(patch, correlationIdentifier);
            }*/

            if (patch.ResourceIdentifier.SchemaIdentifier.Equals(SchemaIdentifiers.Core2Group))
            {
                return this.groupProvider.UpdateAsync(patch, correlationIdentifier);
            }

            throw new NotImplementedException();
        }
    }
}
