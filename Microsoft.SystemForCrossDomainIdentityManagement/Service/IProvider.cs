// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

namespace Microsoft.SCIM
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProvider<TResource> where TResource : Schematized
    {
        bool AcceptLargeObjects { get; set; }
        ServiceConfigurationBase Configuration { get; }
        //IEventTokenHandler EventHandler { get; set; }
        IReadOnlyCollection<IExtension> Extensions { get; }
        IReadOnlyCollection<Core2ResourceType> ResourceTypes { get; }
        IReadOnlyCollection<TypeScheme> Schema { get; }
        //Action<IApplicationBuilder, HttpConfiguration> StartupBehavior { get; }
        Task<TResource> CreateAsync(IRequest<TResource> request);
        Task<TResource> DeleteAsync(IRequest<IResourceIdentifier> request);
        Task<QueryResponse<TResource>> PaginateQueryAsync(IRequest<IQueryParameters> request);
        Task<TResource[]> QueryAsync(IRequest<IQueryParameters> request);
        Task<TResource> ReplaceAsync(IRequest<TResource> request);
        Task<TResource> RetrieveAsync(IRequest<IResourceRetrievalParameters> request);
        Task<TResource> UpdateAsync(IRequest<IPatch> request);
        Task<BulkResponse2> ProcessAsync(IRequest<BulkRequest2> request);
    }
}
