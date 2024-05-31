// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

namespace Microsoft.SCIM
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route(ServiceConstants.RouteResourceTypes)]
    [Authorize]
    [ApiController]
    public sealed class ResourceTypesController : ControllerTemplate<Resource>
    {
        public ResourceTypesController(IProvider<Resource> provider, IMonitor monitor)
            : base(provider, monitor)
        {
        }

        public ActionResult<QueryResponse<Core2ResourceType>> Get()
        {
            string correlationIdentifier = null;

            try
            {
                HttpContext httpContext = this.HttpContext;
                if (!httpContext.TryGetRequestIdentifier(out correlationIdentifier))
                {
                    return this.StatusCode((int)HttpStatusCode.InternalServerError);
                }

                IProvider<Resource> provider = this.provider;
                if (null == provider)
                {
                    return this.StatusCode((int)HttpStatusCode.InternalServerError);
                }

                IReadOnlyCollection<Core2ResourceType> resources = provider.ResourceTypes;
                QueryResponse<Core2ResourceType> result = new QueryResponse<Core2ResourceType>(ProtocolSchemaIdentifiers.Version2ListResponse, resources);

                result.TotalResults =
                    result.ItemsPerPage =
                        resources.Count;
                result.StartIndex = 1;
                return this.Ok(result);

            }
            catch (ArgumentException argumentException)
            {
                if (this.TryGetMonitor(out IMonitor monitor))
                {
                    IExceptionNotification notification =
                        ExceptionNotificationFactory.Instance.CreateNotification(
                            argumentException,
                            correlationIdentifier,
                            ServiceNotificationIdentifiers.ResourceTypesControllerGetArgumentException);
                    monitor.Report(notification);
                }
                return this.BadRequest();
            }
            catch (NotImplementedException notImplementedException)
            {
                if (this.TryGetMonitor(out IMonitor monitor))
                {
                    IExceptionNotification notification =
                        ExceptionNotificationFactory.Instance.CreateNotification(
                            notImplementedException,
                            correlationIdentifier,
                            ServiceNotificationIdentifiers.ResourceTypesControllerGetNotImplementedException);
                    monitor.Report(notification);
                }

                return this.StatusCode((int)HttpStatusCode.NotImplemented);
            }
            catch (NotSupportedException notSupportedException)
            {
                if (this.TryGetMonitor(out IMonitor monitor))
                {
                    IExceptionNotification notification =
                       ExceptionNotificationFactory.Instance.CreateNotification(
                           notSupportedException,
                           correlationIdentifier,
                           ServiceNotificationIdentifiers.ResourceTypesControllerGetNotSupportedException);
                    monitor.Report(notification);
                }

                return this.StatusCode((int)HttpStatusCode.NotImplemented);
            }
            catch (Exception exception)
            {
                if (this.TryGetMonitor(out IMonitor monitor))
                {
                    IExceptionNotification notification =
                       ExceptionNotificationFactory.Instance.CreateNotification(
                           exception,
                           correlationIdentifier,
                           ServiceNotificationIdentifiers.ResourceTypesControllerGetException);
                    monitor.Report(notification);
                }

                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
