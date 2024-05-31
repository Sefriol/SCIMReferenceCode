//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Text.Json.Serialization;
    public sealed class Core2ServiceConfiguration : ServiceConfigurationBase
    {
        public Core2ServiceConfiguration(
            BulkRequestsFeature bulkRequestsSupport,
            bool supportsEntityTags,
            bool supportsFiltering,
            bool supportsPasswordChange,
            bool supportsPatching,
            bool supportsSorting)
        {
            this.AddSchema(SchemaIdentifiers.Core2ServiceConfiguration);
            this.Metadata =
                new Core2Metadata()
                {
                    ResourceType = Types.ServiceProviderConfiguration
                };

            this.BulkRequests = bulkRequestsSupport;
            this.EntityTags = new Feature(supportsEntityTags);
            this.Filtering = new Feature(supportsFiltering);
            this.PasswordChange = new Feature(supportsPasswordChange);
            this.Patching = new Feature(supportsPatching);
            this.Sorting = new Feature(supportsSorting);
        }

        [JsonPropertyName(AttributeNames.Metadata)]
        public Core2Metadata Metadata
        {
            get;
            set;
        }
    }
}
