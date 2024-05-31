//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json.Serialization;

public sealed class BulkRequestOperation : BulkOperation, IJsonOnDeserialized
{
        private Uri path;

        [JsonPropertyName(ProtocolAttributeNames.Path), JsonPropertyOrder(0)]
        public string pathValue;

        private BulkRequestOperation()
        {
        }

        [JsonPropertyName(ProtocolAttributeNames.Data), JsonPropertyOrder(4)]
        public object Data
        {
            get;
            set;
        }

        public Uri Path
        {
            get => this.path;

            set
            {
                this.path = value;
                this.pathValue = new SystemForCrossDomainIdentityManagementResourceIdentifier(value).RelativePath;
            }
        }

        public static BulkRequestOperation CreateDeleteOperation(Uri resource) =>
            new BulkRequestOperation
            {
                Method = HttpMethod.Delete,
                Path = resource ?? throw new ArgumentNullException(nameof(resource))
            };

        public static BulkRequestOperation CreatePatchOperation(Uri resource, PatchRequest2 data)
        {
            ArgumentNullException.ThrowIfNull(resource);

            ArgumentNullException.ThrowIfNull(data);

            PatchRequest2 patchRequest = new PatchRequest2(data.Operations);
            BulkRequestOperation result = new BulkRequestOperation
            {
                Method = ProtocolExtensions.PatchMethod,
                Path = resource,
                Data = patchRequest
            };
            return result;
        }

        public static BulkRequestOperation CreatePostOperation(Resource data)
        {
            ArgumentNullException.ThrowIfNull(data);

            if (null == data.Schemas)
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementProtocolResources.ExceptionUnidentifiableSchema);
            }

            if (!data.Schemas.Any())
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementProtocolResources.ExceptionUnidentifiableSchema);
            }

            IList<Uri> paths = new List<Uri>(1);
            IEnumerable<ISchemaIdentifier> schemaIdentifiers =
                data
                .Schemas
                .Select(
                    (string item) =>
                        new SchemaIdentifier(item));
            foreach (ISchemaIdentifier schemaIdentifier in schemaIdentifiers)
            {
                Uri schemaIdentifierPath = null;
                if (schemaIdentifier.TryFindPath(out string pathValue))
                {
                    schemaIdentifierPath = new Uri(pathValue, UriKind.Relative);
                    if
                    (
                        !paths
                        .Any(
                            (Uri item) =>
                                0 == Uri.Compare(
                                        item,
                                        schemaIdentifierPath,
                                        UriComponents.AbsoluteUri,
                                        UriFormat.UriEscaped,
                                        StringComparison.OrdinalIgnoreCase))
                    )
                    {
                        paths.Add(schemaIdentifierPath);
                    }
                }

                if (data.TryGetPathIdentifier(out Uri resourcePath))
                {
                    if
                   (
                       !paths
                       .Any(
                           (Uri item) =>
                               0 == Uri.Compare(
                                       item,
                                       resourcePath,
                                       UriComponents.AbsoluteUri,
                                       UriFormat.UriEscaped,
                                       StringComparison.OrdinalIgnoreCase))
                   )
                    {
                        paths.Add(resourcePath);
                    }
                }
            }

            if (paths.Count != 1)
            {
                string schemas = string.Join(Environment.NewLine, data.Schemas);
                throw new NotSupportedException(schemas);
            }

            BulkRequestOperation result = new BulkRequestOperation
            {
                path = paths.Single(),
                Method = HttpMethod.Post,
                Data = data
            };
            return result;
        }

        private void InitializePath(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                this.path = null;
                return;
            }

            this.path = new Uri(value, UriKind.Relative);
        }

        private void InitializePath() => this.InitializePath(this.pathValue);

        public void OnDeserialized() => this.InitializePath();
    }
}
