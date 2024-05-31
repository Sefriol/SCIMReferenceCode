// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

namespace Microsoft.SCIM.WebHostSample.Provider
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.SCIM;
    using Microsoft.SCIM.WebHostSample.Resources;

    public class InMemoryProvider : ProviderBase<Resource>
    {

        private static readonly Lazy<IReadOnlyCollection<TypeScheme>> TypeSchema =
            new Lazy<IReadOnlyCollection<TypeScheme>>(
                () =>
                    new TypeScheme[]
                    {
                        SampleTypeScheme.UserTypeScheme,
                        SampleTypeScheme.GroupTypeScheme,
                        SampleTypeScheme.EnterpriseUserTypeScheme,
                        SampleTypeScheme.ResourceTypesTypeScheme,
                        SampleTypeScheme.SchemaTypeScheme,
                        SampleTypeScheme.ServiceProviderConfigTypeScheme
                    });

        private static readonly Lazy<IReadOnlyCollection<Core2ResourceType>> Types =
            new Lazy<IReadOnlyCollection<Core2ResourceType>>(
                () =>
                    new Core2ResourceType[] { SampleResourceTypes.UserResourceType, SampleResourceTypes.GroupResourceType } );


        public InMemoryProvider()
        {
        }

        public override IReadOnlyCollection<Core2ResourceType> ResourceTypes => InMemoryProvider.Types.Value;

        public override IReadOnlyCollection<TypeScheme> Schema => InMemoryProvider.TypeSchema.Value;
        public override Task<Resource> CreateAsync(Resource resource, string correlationIdentifier)
        {
            throw new NotImplementedException();
        }

        public override Task<Resource> DeleteAsync(IResourceIdentifier resourceIdentifier, string correlationIdentifier)
        {
            throw new NotImplementedException();
        }

        public override Task<Resource> RetrieveAsync(IResourceRetrievalParameters parameters, string correlationIdentifier)
        {
            throw new NotImplementedException();
        }

        public override Task<Resource> UpdateAsync(IPatch patch, string correlationIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
