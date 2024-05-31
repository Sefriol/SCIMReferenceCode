//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    public sealed class ExtensionAttributeWindowsAzureActiveDirectoryGroup
    {
        [JsonPropertyName(AttributeNames.ElectronicMailAddresses)]
        public IEnumerable<ElectronicMailAddress> ElectronicMailAddresses
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.ExternalIdentifier)]
        public string ExternalIdentifier
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.MailEnabled)]
        public bool MailEnabled
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.SecurityEnabled)]
        public bool SecurityEnabled
        {
            get;
            set;
        }
    }
}
