//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Text.Json.Serialization;
    public abstract class Resource : Schematized
    {
        [JsonPropertyName(AttributeNames.ExternalIdentifier), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string ExternalIdentifier
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Identifier)]
        public string Identifier
        {
            get;
            set;
        }

        public virtual bool TryGetIdentifier(Uri baseIdentifier, out Uri identifier)
        {
            identifier = null;
            return false;
        }

        public virtual bool TryGetPathIdentifier(out Uri pathIdentifier)
        {
            pathIdentifier = null;
            return false;
        }
    }
}
