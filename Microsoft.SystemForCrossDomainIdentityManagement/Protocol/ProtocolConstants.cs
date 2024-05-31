//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------


namespace Microsoft.SCIM
{
    using System;
    using System.Text.Json;

    public static class ProtocolConstants
    {
        public const string ContentType = "application/scim+json";
        public const string PathGroups = "Groups";
        public const string PathUsers = "Users";
        public const string PathBulk = "Bulk";
        public const string PathWebBatchInterface = SchemaConstants.PathInterface + "/batch";

        public readonly static Lazy<JsonSerializerOptions> JsonSettings =
            new Lazy<JsonSerializerOptions>(() => ProtocolConstants.InitializeSettings());

        public readonly static Lazy<JsonSerializerOptions> ScimPatchValueSettings =
            new Lazy<JsonSerializerOptions>(() => ProtocolConstants.InitializeScimPatchValueSettings());

        private static JsonSerializerOptions InitializeSettings()
        {
            JsonSerializerOptions result = new JsonSerializerOptions
            {
                AllowTrailingCommas = true
            };
            return result;
        }

        private static JsonSerializerOptions InitializeScimPatchValueSettings()
        {
            JsonSerializerOptions result = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                Converters = { new ScimPatchValueConverter() }
            };
            return result;
        }
    }
}
