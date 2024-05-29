namespace SpiceDB.SCIM.Provider.Provider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Authzed.Api.V1;
    using Grpc.Core;
    using Grpc.Core.Utils;
    using Microsoft.SCIM;
    using Extensions;

    public class SpiceDbGroupProvider : ProviderBase
    {
        private readonly PermissionsService.PermissionsServiceClient permissionClient;

        public SpiceDbGroupProvider(PermissionsService.PermissionsServiceClient permissionsServiceClient)
        {
            this.permissionClient = permissionsServiceClient;
        }


        public override async Task<Resource> CreateAsync(Resource resource, string correlationIdentifier)
        {
            if (resource.Identifier != null)
            {
                throw new CustomHttpResponseException(HttpStatusCode.BadRequest);
            }

            Core2Group group = resource as Core2Group;

            if (group == null || !group.Members.Any())
            {
                throw new ScimTypeException(ErrorType.invalidValue,
                    "SpiceDB relations require at least one group member. Empty Groups are not allowed");
            }

            var relationshipStream = permissionClient.ReadRelationships(new ReadRelationshipsRequest
            {
                RelationshipFilter = new RelationshipFilter
                {
                    ResourceType = "group",
                    OptionalResourceId = group.ExternalIdentifier,
                },
                Consistency = new Consistency
                {
                    FullyConsistent = true
                },
                OptionalLimit = 1
            });

            try
            {
                var list = await relationshipStream.ResponseStream.ToListAsync();
                if (list.Count != 0)
                {
                    throw new ScimTypeException(ErrorType.uniqueness, "Group already exists");
                }
            }
            catch (RpcException e)
            {
                if (e.StatusCode == StatusCode.InvalidArgument)
                {
                    throw new ScimTypeException(ErrorType.invalidValue,
                        "External identifier should match regex pattern \"^([a-zA-Z0-9/_|\\\\-=+]{1,})?$\"");
                }

                throw;
            }


            var createRelationshipRequest = new WriteRelationshipsRequest();

            foreach (var member in group.Members)
            {
                if (member.TypeName is not ("user" or "group"))
                {
                    throw new ScimTypeException(ErrorType.invalidValue,
                        $"SpiceDB group members should be of type 'user' or 'group', but received '{member.TypeName}'");
                }

                createRelationshipRequest.Updates.Add(new RelationshipUpdate
                    {
                        Operation = RelationshipUpdate.Types.Operation.Create,
                        Relationship = new Relationship
                        {
                            Resource = new ObjectReference
                            {
                                ObjectType = "group",
                                ObjectId = group.ExternalIdentifier
                            },
                            Relation = "member",
                            Subject = new SubjectReference
                            {
                                Object = new ObjectReference
                                {
                                    ObjectType = member.TypeName,
                                    ObjectId = member.Value
                                },
                            },
                        }
                    }
                );
            }

            await permissionClient.WriteRelationshipsAsync(createRelationshipRequest);

            resource.Identifier = resource.ExternalIdentifier;
            return resource;
        }

        public override async Task<Resource> DeleteAsync(IResourceIdentifier resourceIdentifier,
            string correlationIdentifier)
        {
            if (string.IsNullOrWhiteSpace(resourceIdentifier?.Identifier))
            {
                throw new CustomHttpResponseException(HttpStatusCode.BadRequest);
            }

            string identifier = resourceIdentifier.Identifier;

            var deleteRequest = new DeleteRelationshipsRequest
            {
                RelationshipFilter = new RelationshipFilter
                {
                    ResourceType = "group",
                    OptionalResourceId = identifier,
                },
                OptionalLimit = 0,
                OptionalAllowPartialDeletions = false
            };
            var results = await permissionClient.DeleteRelationshipsAsync(deleteRequest);
            if (results.DeletionProgress != DeleteRelationshipsResponse.Types.DeletionProgress.Complete)
            {
                throw new CustomHttpResponseException(HttpStatusCode.InternalServerError);
            }

            return new Core2Group
            {
                Identifier = identifier
            };
        }

        public override async Task<Resource[]> QueryAsync(IQueryParameters parameters, string correlationIdentifier)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (string.IsNullOrWhiteSpace(correlationIdentifier))
            {
                throw new ArgumentNullException(nameof(correlationIdentifier));
            }

            if (null == parameters.AlternateFilters)
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources
                    .ExceptionInvalidParameters);
            }

            if (string.IsNullOrWhiteSpace(parameters.SchemaIdentifier))
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources
                    .ExceptionInvalidParameters);
            }

            Dictionary<string, Core2Group> results = new Dictionary<string, Core2Group>();
            IFilter queryFilter = parameters.AlternateFilters.SingleOrDefault();

            if (queryFilter == null)
            {
                var relationshipStream = permissionClient.ReadRelationships(new ReadRelationshipsRequest
                {
                    RelationshipFilter = new RelationshipFilter
                    {
                        ResourceType = "group",
                    },
                    Consistency = new Consistency
                    {
                        FullyConsistent = true
                    }
                });
                await foreach (var relationship in relationshipStream.ResponseStream.ReadAllAsync())
                {
                    if (results.TryGetValue(relationship.Relationship.Resource.ObjectId, out var existingGroup))
                    {
                        existingGroup.Members = existingGroup.Members.Append(new Member
                        {
                            TypeName = relationship.Relationship.Subject.Object.ObjectType,
                            Value = relationship.Relationship.Subject.Object.ObjectId
                        });
                        continue;
                    }

                    results.Add(relationship.Relationship.Resource.ObjectId, new Core2Group
                    {
                        Identifier = relationship.Relationship.Resource.ObjectId,
                        DisplayName = relationship.Relationship.Resource.ObjectId,
                        ExternalIdentifier = relationship.Relationship.Resource.ObjectId,
                        Members = new[]
                        {
                            new Member
                            {
                                TypeName = relationship.Relationship.Subject.Object.ObjectType,
                                Value = relationship.Relationship.Subject.Object.ObjectId
                            }
                        }
                    });
                }
            }
            else
            {
                throw new ScimTypeException(ErrorType.invalidFilter,
                    string.Format(
                        SystemForCrossDomainIdentityManagementServiceResources
                            .ExceptionFilterAttributePathNotSupportedTemplate, queryFilter.AttributePath));
            }

            return results.Values.ToArray();
        }

        public override async Task<Resource> ReplaceAsync(Resource resource, string correlationIdentifier)
        {
            if (resource.Identifier == null)
            {
                throw new CustomHttpResponseException(HttpStatusCode.BadRequest);
            }

            Core2Group group = resource as Core2Group;

            var createRelationshipRequest = new WriteRelationshipsRequest();

            foreach (var member in group.Members)
            {
                if (member.TypeName != "user" || member.TypeName != "group")
                {
                    throw new ScimTypeException(ErrorType.invalidValue,
                        $"SpiceDB group members should be of type 'user' or 'group', but received '{member.TypeName}'");
                }

                createRelationshipRequest.Updates.Add(new RelationshipUpdate
                    {
                        Operation = RelationshipUpdate.Types.Operation.Create,
                        Relationship = new Relationship
                        {
                            Resource = new ObjectReference
                            {
                                ObjectType = "group",
                                ObjectId = group.ExternalIdentifier
                            },
                            Relation = "member",
                            Subject = new SubjectReference
                            {
                                Object = new ObjectReference
                                {
                                    ObjectType = member.TypeName,
                                    ObjectId = member.Value
                                },
                            },
                        }
                    }
                );
            }

            var deleteRequest = new DeleteRelationshipsRequest
            {
                RelationshipFilter = new RelationshipFilter
                {
                    ResourceType = "group",
                    OptionalResourceId = resource.ExternalIdentifier,
                    OptionalSubjectFilter = new SubjectFilter
                    {
                        SubjectType = "user"
                    }
                },
                OptionalLimit = 0,
                OptionalAllowPartialDeletions = false
            };
            var results = await permissionClient.DeleteRelationshipsAsync(deleteRequest);
            if (results.DeletionProgress != DeleteRelationshipsResponse.Types.DeletionProgress.Complete)
            {
                throw new CustomHttpResponseException(HttpStatusCode.InternalServerError);
            }

            await permissionClient.WriteRelationshipsAsync(createRelationshipRequest);

            resource.Identifier = resource.ExternalIdentifier;
            return group;
        }

        public override async Task<Resource> RetrieveAsync(IResourceRetrievalParameters parameters,
            string correlationIdentifier)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (string.IsNullOrWhiteSpace(correlationIdentifier))
            {
                throw new ArgumentNullException(nameof(correlationIdentifier));
            }

            if (string.IsNullOrEmpty(parameters?.ResourceIdentifier?.Identifier))
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            string identifier = parameters.ResourceIdentifier.Identifier;

            var relationshipStream = permissionClient.ReadRelationships(new ReadRelationshipsRequest
            {
                RelationshipFilter = new RelationshipFilter
                {
                    ResourceType = "group",
                    OptionalResourceId = identifier,
                }
            });

            Core2Group group = null;
            await foreach (var relationship in relationshipStream.ResponseStream.ReadAllAsync())
            {
                if (group == null)
                {
                    group = new Core2Group
                    {
                        Identifier = relationship.Relationship.Resource.ObjectId,
                        DisplayName = relationship.Relationship.Resource.ObjectId,
                        ExternalIdentifier = relationship.Relationship.Resource.ObjectId,
                        Members = new[]
                        {
                            new Member()
                            {
                                TypeName = relationship.Relationship.Subject.Object.ObjectType,
                                Value = relationship.Relationship.Subject.Object.ObjectId
                            }
                        }
                    };
                    continue;
                }

                group.Members = group.Members.Append(new Member
                {
                    TypeName = relationship.Relationship.Subject.Object.ObjectType,
                    Value = relationship.Relationship.Subject.Object.ObjectId
                });
            }

            if (group == null)
            {
                throw new CustomHttpResponseException(HttpStatusCode.NotFound);
            }

            return group;
        }

        public override async Task<Resource> UpdateAsync(IPatch patch, string correlationIdentifier)
        {
            ArgumentNullException.ThrowIfNull(patch);

            if (null == patch.ResourceIdentifier)
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources
                    .ExceptionInvalidOperation);
            }

            if (string.IsNullOrWhiteSpace(patch.ResourceIdentifier.Identifier))
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources
                    .ExceptionInvalidOperation);
            }

            if (null == patch.PatchRequest)
            {
                throw new ArgumentException(SystemForCrossDomainIdentityManagementServiceResources
                    .ExceptionInvalidOperation);
            }

            PatchRequest2 patchRequest =
                patch.PatchRequest as PatchRequest2;

            if (null == patchRequest)
            {
                string unsupportedPatchTypeName = patch.GetType().FullName;
                throw new NotSupportedException(unsupportedPatchTypeName);
            }

            var writeRelationshipsRequest = new WriteRelationshipsRequest()
            {
                Updates = { patchRequest.Patch2Update(patch.ResourceIdentifier.Identifier) }
            };

            await permissionClient.WriteRelationshipsAsync(writeRelationshipsRequest);

            // Return updated group
            var relationshipStream = permissionClient.ReadRelationships(new ReadRelationshipsRequest
            {
                RelationshipFilter = new RelationshipFilter
                {
                    ResourceType = "group",
                    OptionalResourceId = patch.ResourceIdentifier.Identifier,
                }
            });

            Core2Group group = null;
            await foreach (var relationship in relationshipStream.ResponseStream.ReadAllAsync())
            {
                if (group == null)
                {
                    group = new Core2Group
                    {
                        Identifier = relationship.Relationship.Resource.ObjectId,
                        DisplayName = relationship.Relationship.Resource.ObjectId,
                        ExternalIdentifier = relationship.Relationship.Resource.ObjectId,
                        Members = new[]
                        {
                            new Member()
                            {
                                TypeName = relationship.Relationship.Subject.Object.ObjectType,
                                Value = relationship.Relationship.Subject.Object.ObjectId
                            }
                        }
                    };
                    continue;
                }

                group.Members = group.Members.Append(new Member
                {
                    TypeName = relationship.Relationship.Subject.Object.ObjectType,
                    Value = relationship.Relationship.Subject.Object.ObjectId
                });
            }

            if (group == null)
            {
                // All members might have been removed, but some other relations might still exist
                group = new Core2Group
                {
                    Identifier = patch.ResourceIdentifier.Identifier,
                    ExternalIdentifier = patch.ResourceIdentifier.Identifier,
                    Members = Array.Empty<Member>()
                };
            }

            return group;
        }
    }
}
