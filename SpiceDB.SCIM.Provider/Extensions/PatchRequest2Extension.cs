using Authzed.Api.V1;
using Microsoft.SCIM;
using Newtonsoft.Json;

namespace SpiceDB.SCIM.Provider.Extensions;

public static class PatchRequest2Extension
{
    public static RelationshipUpdate[] Patch2Update(this PatchRequest2 patch, string resourceId)
    {
        List<RelationshipUpdate> updates = [];
        if (null == patch)
        {
            return updates.ToArray();
        }

        if (null == patch.Operations || !patch.Operations.Any())
        {
            return updates.ToArray();
        }

        foreach (PatchOperation2Combined operation in patch.Operations)
        {
            PatchOperation2 operationInternal = new PatchOperation2()
            {
                OperationName = operation.OperationName,
                Path = operation.Path
            };

            OperationValue[] values = null;
            if (operation?.Value != null)
            {
                values =
                    JsonConvert.DeserializeObject<OperationValue[]>(
                        operation.Value,
                        ProtocolConstants.JsonSettings.Value);
            }

            if (values == null)
            {
                string value = null;
                if (operation?.Value != null)
                {
                    value = JsonConvert.DeserializeObject<string>(operation.Value,
                        ProtocolConstants.JsonSettings.Value);
                }

                OperationValue valueSingle = new OperationValue()
                {
                    Value = value
                };
                operationInternal.AddValue(valueSingle);
            }
            else
            {
                foreach (OperationValue value in values)
                {
                    operationInternal.AddValue(value);
                }
            }

            var update = Patch2Update(operationInternal, resourceId);
            if (update != null)
            {
                updates.AddRange(update);
            }
        }

        return updates.ToArray();
    }

    private static List<RelationshipUpdate> Patch2Update(this PatchOperation2 operation, string resourceId)
    {
        List<RelationshipUpdate> updates = [];
        if (operation?.Path == null || string.IsNullOrWhiteSpace(operation.Path.AttributePath))
        {
            return updates;
        }

        switch (operation.Path.AttributePath)
        {
            case AttributeNames.Members:
                if (operation.Value != null)
                {
                    switch (operation.Name)
                    {
                        case OperationName.Remove:
                        case OperationName.Add:
                            foreach (var member in operation.Value)
                            {
                                updates.Add(new RelationshipUpdate
                                {
                                    Operation = operation.Name == OperationName.Remove
                                        ? RelationshipUpdate.Types.Operation.Delete
                                        : RelationshipUpdate.Types.Operation.Touch,
                                    Relationship = new Relationship
                                    {
                                        Resource = new ObjectReference
                                        {
                                            ObjectType = "group",
                                            ObjectId = resourceId
                                        },
                                        Relation = "member",
                                        Subject = new SubjectReference
                                        {
                                            Object = new ObjectReference
                                            {
                                                ObjectType = "user",
                                                ObjectId = member.Value
                                            },
                                        }
                                    }
                                });
                            }

                            break;
                        case OperationName.Replace:
                            throw new NotImplementedException("Patch Operation Replace is not implemented.");
                    }
                }

                break;
        }

        return updates;
    }
}
