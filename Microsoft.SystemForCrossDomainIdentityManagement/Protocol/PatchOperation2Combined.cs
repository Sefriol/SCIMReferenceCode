//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text.Json.Serialization;

    public sealed class PatchOperation2Combined : PatchOperation2Base, IJsonOnDeserialized
    {
        private const string Template = "{0}: [{1}]";

        [JsonPropertyName(AttributeNames.Value), JsonInclude, JsonPropertyOrder(2)]
        internal object values;


        public PatchOperation2Combined()
        {
        }

        public PatchOperation2Combined(OperationName operationName, string pathExpression)
            : base(operationName, pathExpression)
        {
        }
        public static PatchOperation2Combined Create(OperationName operationName, string pathExpression, string value)
        {
            if (string.IsNullOrWhiteSpace(pathExpression))
            {
                throw new ArgumentNullException(nameof(pathExpression));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            OperationValue operationValue = new OperationValue();
            operationValue.Value = value;

            PatchOperation2Combined result = new PatchOperation2Combined(operationName, pathExpression);
            result.Value = System.Text.Json.JsonSerializer.Serialize(operationValue);

            return result;
        }

        [JsonIgnore]
        public string Value
        {
            get
            {
                if (this.values == null)
                {
                    return null;
                }

                string result = System.Text.Json.JsonSerializer.Serialize(this.values);
                return result;
            }

            set { this.values = value; }
        }

        public void OnDeserialized()
        {
            if (this.Value == null)
            {
                if
                (
                    this?.Path?.AttributePath != null &&
                    this.Path.AttributePath.Contains(AttributeNames.Members, StringComparison.OrdinalIgnoreCase) &&
                    this.Name == SCIM.OperationName.Remove &&
                    this.Path?.SubAttributes?.Count == 1
                )
                {
                    this.Value = this.Path.SubAttributes.First().ComparisonValue;
                    IPath path = SCIM.Path.Create(AttributeNames.Members);
                    this.Path = path;
                }
            }
        }

        public override string ToString()
        {
            string allValues = string.Join(Environment.NewLine, this.Value);
            string operation = base.ToString();
            string result =
                string.Format(
                    CultureInfo.InvariantCulture,
                    PatchOperation2Combined.Template,
                    operation,
                    allValues);
            return result;
        }
    }
}
