//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Linq;

    using Newtonsoft.Json.Linq;

    public static class ObjectExtentions
    {
        public static bool IsResourceType(this object json, string scheme)
        {
            ArgumentNullException.ThrowIfNull(json);
            if (string.IsNullOrWhiteSpace(scheme))
            {
                throw new ArgumentNullException(nameof(scheme));
            }

            dynamic operationDataJson = System.Text.Json.JsonSerializer.Deserialize<dynamic>(json.ToString());
            bool result = false;

            switch (operationDataJson.schemas)
            {
                case JArray schemas:
                    string[] schemasList = schemas.ToObject<string[]>();
                    result =
                        schemasList
                        .Any(
                            (string item) =>
                                string.Equals(item, scheme, StringComparison.OrdinalIgnoreCase));
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
