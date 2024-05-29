// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

namespace Microsoft.SCIM
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProvider
    {
        bool AcceptLargeObjects { get; set; }
        ServiceConfigurationBase Configuration { get; }
        //IEventTokenHandler EventHandler { get; set; }
        IReadOnlyCollection<IExtension> Extensions { get; }
        IReadOnlyCollection<Core2ResourceType> ResourceTypes { get; }
        IReadOnlyCollection<TypeScheme> Schema { get; }
        //Action<IApplicationBuilder, HttpConfiguration> StartupBehavior { get; }
        Task<Resource> CreateAsync(IRequest<Resource> request);
        Task<Resource> DeleteAsync(IRequest<IResourceIdentifier> request);
        Task<QueryResponse> PaginateQueryAsync(IRequest<IQueryParameters> request);
        Task<Resource[]> QueryAsync(IRequest<IQueryParameters> request);
        Task<Resource> ReplaceAsync(IRequest<Resource> request);
        Task<Resource> RetrieveAsync(IRequest<IResourceRetrievalParameters> request);
        Task<Resource> UpdateAsync(IRequest<IPatch> request);
        Task<BulkResponse2> ProcessAsync(IRequest<BulkRequest2> request);
    }
}
