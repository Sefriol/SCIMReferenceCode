// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

namespace Microsoft.SCIM
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route(ServiceConstants.RouteSchemas)]
    [Authorize]
    [ApiController]
    public sealed class SchemasController : ControllerTemplate<Resource>
    {
        public SchemasController(IProvider<Resource> provider, IMonitor monitor)
            : base(provider, monitor)
        {
        }

        public ActionResult<QueryResponse<TypeScheme>> Get()
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

                IReadOnlyCollection<TypeScheme> resources = provider.Schema;
                QueryResponse<TypeScheme> result = new QueryResponse<TypeScheme>(ProtocolSchemaIdentifiers.Version2ListResponse, resources);

                var test = System.Text.Json.JsonSerializer.Serialize(resources);
                result.TotalResults =
                    result.ItemsPerPage =
                        resources.Count;
                result.StartIndex = 1;
                return result;

            }
            catch (ArgumentException argumentException)
            {
                if (this.TryGetMonitor(out IMonitor monitor))
                {
                    IExceptionNotification notification =
                        ExceptionNotificationFactory.Instance.CreateNotification(
                            argumentException,
                            correlationIdentifier,
                            ServiceNotificationIdentifiers.SchemasControllerGetArgumentException);
                    monitor.Report(notification);
                }

                return BadRequest();
            }
            catch (NotImplementedException notImplementedException)
            {
                if (this.TryGetMonitor(out IMonitor monitor))
                {
                    IExceptionNotification notification =
                        ExceptionNotificationFactory.Instance.CreateNotification(
                            notImplementedException,
                            correlationIdentifier,
                            ServiceNotificationIdentifiers.SchemasControllerGetNotImplementedException);
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
                            ServiceNotificationIdentifiers.SchemasControllerGetNotSupportedException);
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
                            ServiceNotificationIdentifiers.SchemasControllerGetException);
                    monitor.Report(notification);
                }

                return this.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
