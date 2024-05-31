//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Globalization;
    using System.Text.Json.Serialization;
    public sealed class OperationValue
    {
        private const string Template = "{0} {1}";

        [JsonPropertyName(ProtocolAttributeNames.Reference), JsonPropertyOrder(0), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Reference
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Value), JsonPropertyOrder(1), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Value
        {
            get;
            set;
        }

        public override string ToString()
        {
            string result =
                string.Format(
                    CultureInfo.InvariantCulture,
                    OperationValue.Template,
                    this.Value,
                    this.Reference)
                .Trim();
            return result;
        }
    }
}
