// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

namespace Microsoft.SCIM
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    public abstract class ProviderBase<TResource> : IProvider<TResource> where TResource : Resource
    {
        private static readonly Lazy<BulkRequestsFeature> BulkFeatureSupport =
            new Lazy<BulkRequestsFeature>(
                () =>
                    BulkRequestsFeature.CreateUnsupportedFeature());

        private static readonly Lazy<IReadOnlyCollection<TypeScheme>> TypeSchema =
            new Lazy<IReadOnlyCollection<TypeScheme>>(
                () =>
                    Array.Empty<TypeScheme>());

        private static readonly Lazy<ServiceConfigurationBase> ServiceConfiguration =
            new Lazy<ServiceConfigurationBase>(
                () =>
                    new Core2ServiceConfiguration(ProviderBase<TResource>.BulkFeatureSupport.Value, false, true, false, true, false));

        private static readonly Lazy<IReadOnlyCollection<Core2ResourceType>> Types =
            new Lazy<IReadOnlyCollection<Core2ResourceType>>(
                () =>
                    Array.Empty<Core2ResourceType>());

        public virtual bool AcceptLargeObjects
        {
            get;
            set;
        }

        public virtual ServiceConfigurationBase Configuration
        {
            get
            {
                return ProviderBase<TResource>.ServiceConfiguration.Value;
            }
        }

        //public virtual IEventTokenHandler EventHandler
        //{
        //    get;
        //    set;
        //}

        public virtual IReadOnlyCollection<IExtension> Extensions
        {
            get
            {
                return null;
            }
        }

        public virtual IReadOnlyCollection<Core2ResourceType> ResourceTypes
        {
            get
            {
                return ProviderBase<TResource>.Types.Value;
            }
        }

        public virtual IReadOnlyCollection<TypeScheme> Schema
        {
            get
            {
                return ProviderBase<TResource>.TypeSchema.Value;
            }
        }

        //public virtual Action<IAppBuilder, HttpConfiguration> StartupBehavior
        //{
        //    get
        //    {
        //        return null;
        //    }
        //}

        public abstract Task<TResource> CreateAsync(TResource resource, string correlationIdentifier);

        public virtual async Task<TResource> CreateAsync(IRequest<TResource> request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (null == request.Payload)
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources.ExceptionInvalidRequest);
            }

            if (string.IsNullOrWhiteSpace(request.CorrelationIdentifier))
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources.ExceptionInvalidRequest);
            }

            TResource result = await this.CreateAsync(request.Payload, request.CorrelationIdentifier).ConfigureAwait(false);
            return result;
        }

        public abstract Task<TResource> DeleteAsync(IResourceIdentifier resourceIdentifier,
            string correlationIdentifier);

        public virtual async Task<TResource> DeleteAsync(IRequest<IResourceIdentifier> request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (null == request.Payload)
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources.ExceptionInvalidRequest);
            }

            if (string.IsNullOrWhiteSpace(request.CorrelationIdentifier))
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources.ExceptionInvalidRequest);
            }

            return await this.DeleteAsync(request.Payload, request.CorrelationIdentifier).ConfigureAwait(false);
        }

        public virtual async Task<QueryResponse<TResource>> PaginateQueryAsync(IRequest<IQueryParameters> request)
        {
            ArgumentNullException.ThrowIfNull(request);

            IReadOnlyCollection<TResource> resources = await this.QueryAsync(request).ConfigureAwait(false);
            QueryResponse<TResource> result = new QueryResponse<TResource>(ProtocolSchemaIdentifiers.Version2ListResponse, resources);
            result.TotalResults =
                result.ItemsPerPage =
                    resources.Count;
            result.StartIndex = resources.Count != 0 ? 1 : (int?)null;
            return result;
        }


        public  virtual async Task<BulkResponse2> ProcessAsync(IRequest<BulkRequest2> request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (null == request.HttpContext)
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources.ExceptionInvalidRequest);
            }
            Queue<IBulkOperationContext> operations = request.EnqueueOperations();
            BulkResponse2 result = await this.ProcessAsync(operations).ConfigureAwait(false);
            return result;
        }

        public virtual async Task ProcessAsync(IBulkOperationContext operation)
        {
            ArgumentNullException.ThrowIfNull(operation);

            if (!operation.TryPrepare())
            {
                return;
            }

            if (null == operation.Method)
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources.ExceptionInvalidOperation);
            }

            if (null == operation.Operation)
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources.ExceptionInvalidOperation);
            }

            BulkResponseOperation response =
                new BulkResponseOperation(operation.Operation.Identifier)
                {
                    Method = operation.Method
                };

            if (HttpMethod.Delete == operation.Method)
            {
                IBulkOperationContext<IResourceIdentifier> context = (IBulkOperationContext<IResourceIdentifier>)operation;
                await this.DeleteAsync(context.Request).ConfigureAwait(false);
                response.Status = HttpStatusCode.NoContent;
            }
            else if (HttpMethod.Get == operation.Method)
            {
                switch (operation)
                {
                    case IBulkOperationContext<IResourceRetrievalParameters> retrievalContext:
                        response.Response = await this.RetrieveAsync(retrievalContext.Request).ConfigureAwait(false);
                        break;
                    default:
                        IBulkOperationContext<IQueryParameters> queryContext = (IBulkOperationContext<IQueryParameters>)operation;
                        response.Response = await this.QueryAsync(queryContext.Request).ConfigureAwait(false);
                        break;
                }
                response.Status = HttpStatusCode.OK;
            }
            else if (ProtocolExtensions.PatchMethod == operation.Method)
            {
                IBulkOperationContext<IPatch> context = (IBulkOperationContext<IPatch>)operation;
                await this.UpdateAsync(context.Request).ConfigureAwait(false);
                response.Status = HttpStatusCode.OK;
            }
            else if (HttpMethod.Post == operation.Method)
            {
                IBulkOperationContext<TResource> context = (IBulkOperationContext<TResource>)operation;
                TResource output = await this.CreateAsync(context.Request).ConfigureAwait(false);
                response.Status = HttpStatusCode.Created;
                response.Location = output.GetResourceIdentifier(context.BulkRequest.BaseResourceIdentifier);
            }
            else
            {
                string exceptionMessage =
                    string.Format(
                        CultureInfo.InvariantCulture,
                        SystemForCrossDomainIdentityManagementServiceResources.ExceptionMethodNotSupportedTemplate,
                        operation.Method);
                ErrorResponse error =
                    new ErrorResponse()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Detail = exceptionMessage
                    };
                response.Response = error;
                response.Status = HttpStatusCode.BadRequest;
            }

            operation.Complete(response);
        }

        public virtual async Task<BulkResponse2> ProcessAsync(Queue<IBulkOperationContext> operations)
        {
            ArgumentNullException.ThrowIfNull(operations);

            BulkResponse2 result = new BulkResponse2();
            int countFailures = 0;
            while (operations.Any())
            {
                IBulkOperationContext operation = operations.Dequeue();
                await this.ProcessAsync(operation).ConfigureAwait(false);

                bool addOperation;
                switch (operation)
                {
                    case IBulkUpdateOperationContext updateOperation:
                        addOperation = null == updateOperation.Parent;
                        break;
                    default:
                        addOperation = true;
                        break;
                }
                if (addOperation)
                {
                    result.AddOperation(operation.Response);
                }

                if (operation.Response.IsError())
                {
                    checked
                    {
                        countFailures++;
                    }
                }

                if
                (
                        operation.BulkRequest.Payload.FailOnErrors.HasValue
                    && countFailures > operation.BulkRequest.Payload.FailOnErrors.Value
                )
                {
                    break;
                }
            }
            return result;
        }

        public virtual Task<TResource[]> QueryAsync(IQueryParameters parameters, string correlationIdentifier)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TResource[]> QueryAsync(IRequest<IQueryParameters> request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (null == request.Payload)
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources.ExceptionInvalidRequest);
            }

            if (string.IsNullOrWhiteSpace(request.CorrelationIdentifier))
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources.ExceptionInvalidRequest);
            }

            return await this.QueryAsync(request.Payload, request.CorrelationIdentifier).ConfigureAwait(false);
        }

        public virtual Task<TResource> ReplaceAsync(TResource resource, string correlationIdentifier)
        {
            throw new NotSupportedException();
        }

        public virtual async Task<TResource> ReplaceAsync(IRequest<TResource> request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (null == request.Payload)
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources.ExceptionInvalidRequest);
            }

            if (string.IsNullOrWhiteSpace(request.CorrelationIdentifier))
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources.ExceptionInvalidRequest);
            }

            return await this.ReplaceAsync(request.Payload, request.CorrelationIdentifier).ConfigureAwait(false);
        }

        public abstract Task<TResource> RetrieveAsync(IResourceRetrievalParameters parameters,
            string correlationIdentifier);

        public virtual async Task<TResource> RetrieveAsync(IRequest<IResourceRetrievalParameters> request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (null == request.Payload)
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources.ExceptionInvalidRequest);
            }

            if (string.IsNullOrWhiteSpace(request.CorrelationIdentifier))
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources.ExceptionInvalidRequest);
            }

            return await this.RetrieveAsync(request.Payload, request.CorrelationIdentifier).ConfigureAwait(false);
        }

        public abstract Task<TResource> UpdateAsync(IPatch patch, string correlationIdentifier);

        public virtual async Task<TResource> UpdateAsync(IRequest<IPatch> request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (null == request.Payload)
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources.ExceptionInvalidRequest);
            }

            if (string.IsNullOrWhiteSpace(request.CorrelationIdentifier))
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources.ExceptionInvalidRequest);
            }

            return await this.UpdateAsync(request.Payload, request.CorrelationIdentifier).ConfigureAwait(false);
        }
    }
}
