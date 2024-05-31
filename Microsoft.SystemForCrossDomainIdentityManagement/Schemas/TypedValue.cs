//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------



namespace Microsoft.SCIM
{
    using System.Text.Json.Serialization;

    public abstract class TypedValue : TypedItem
    {
        [JsonPropertyName(AttributeNames.Value), JsonPropertyOrder(0)]
        public string Value
        {
            get;
            set;
        }
    }
}
