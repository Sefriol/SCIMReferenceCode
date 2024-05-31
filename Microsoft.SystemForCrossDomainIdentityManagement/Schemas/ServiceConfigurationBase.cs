//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;

    public abstract class ServiceConfigurationBase : Schematized, IJsonOnDeserialized, IJsonOnDeserializing
    {
        [JsonPropertyName(AttributeNames.AuthenticationSchemes), JsonInclude]
        private List<AuthenticationScheme> authenticationSchemes;
        private IReadOnlyCollection<AuthenticationScheme> authenticationSchemesWrapper;

        private object thisLock;

        public ServiceConfigurationBase()
        {
            this.OnInitialization();
            this.OnInitialized();
        }

        [JsonIgnore]
        public IReadOnlyCollection<AuthenticationScheme> AuthenticationSchemes
        {
            get
            {
                return this.authenticationSchemesWrapper;
            }
        }

        [JsonPropertyName(AttributeNames.Bulk)]
        public BulkRequestsFeature BulkRequests
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Documentation)]
        public Uri DocumentationResource
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.EntityTag)]
        public Feature EntityTags
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Filter)]
        public Feature Filtering
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.ChangePassword)]
        public Feature PasswordChange
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Patch)]
        public Feature Patching
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Sort)]
        public Feature Sorting
        {
            get;
            set;
        }

        public void AddAuthenticationScheme(AuthenticationScheme authenticationScheme)
        {
            ArgumentNullException.ThrowIfNull(authenticationScheme);

            if (string.IsNullOrWhiteSpace(authenticationScheme.Name))
            {
                throw new ArgumentException(
                    SystemForCrossDomainIdentityManagementSchemasResources.ExceptionUnnamedAuthenticationScheme);
            }

            Func<bool> containsFunction =
                new Func<bool>(
                        () =>
                            this
                            .authenticationSchemes
                            .Any(
                                (AuthenticationScheme item) =>
                                    string.Equals(item.Name, authenticationScheme.Name, StringComparison.OrdinalIgnoreCase)));


            if (!containsFunction())
            {
                lock (this.thisLock)
                {
                    if (!containsFunction())
                    {
                        this.authenticationSchemes.Add(authenticationScheme);
                    }
                }
            }
        }

        public new void OnDeserialized()
        {
            this.OnInitialized();
        }

        public new void OnDeserializing()
        {
            this.OnInitialization();
        }

        private void OnInitialization()
        {
            this.thisLock = new object();
            this.authenticationSchemes = new List<AuthenticationScheme>();
        }

        private void OnInitialized()
        {
            this.authenticationSchemesWrapper = this.authenticationSchemes.AsReadOnly();
        }
    }
}
