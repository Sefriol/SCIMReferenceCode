// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

namespace Microsoft.SCIM
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProviderAdapter<T> where T : Resource
    {
        string SchemaIdentifier { get; }

        Task<T> Create(HttpContext httpContext, T resource, string correlationIdentifier);
        Task<T> Delete(HttpContext httpContext, string identifier, string correlationIdentifier);
        Task<QueryResponse<T>> Query(
            HttpContext httpContext,
            IReadOnlyCollection<IFilter> filters,
            IReadOnlyCollection<string> requestedAttributePaths,
            IReadOnlyCollection<string> excludedAttributePaths,
            IPaginationParameters paginationParameters,
            string correlationIdentifier);
        Task<T> Replace(HttpContext httpContext, T resource, string correlationIdentifier);
        Task<T> Retrieve(
            HttpContext httpContext,
            string identifier,
            IReadOnlyCollection<string> requestedAttributePaths,
            IReadOnlyCollection<string> excludedAttributePaths,
            string correlationIdentifier);
        Task<T> Update(
            HttpContext httpContext,
            string identifier,
            Schematized patchRequest,
            string correlationIdentifier);
    }
}
