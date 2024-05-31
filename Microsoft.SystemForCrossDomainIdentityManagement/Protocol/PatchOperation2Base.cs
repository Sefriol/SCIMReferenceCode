//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Globalization;
    using System.Text.Json.Serialization;
    public abstract class PatchOperation2Base
    {
        private const string Template = "{0} {1}";

        private OperationName name;
        private string operationName;

        [JsonIgnore]
        private IPath path;
        [JsonPropertyName(ProtocolAttributeNames.Path), JsonInclude, JsonPropertyOrder(1)]
        private string pathExpression;

        public PatchOperation2Base()
        {
        }

        protected PatchOperation2Base(OperationName operationName, string pathExpression)
        {
            if (string.IsNullOrWhiteSpace(pathExpression))
            {
                throw new ArgumentNullException(nameof(pathExpression));
            }

            this.Name = operationName;
            this.Path = Microsoft.SCIM.Path.Create(pathExpression);
        }

        [JsonIgnore]
        public OperationName Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.operationName = Enum.GetName(typeof(OperationName), value);
            }
        }

        // It's the value of 'op' parameter within the json of request body.
        [JsonPropertyName(ProtocolAttributeNames.Patch2Operation), JsonPropertyOrder(0)]
        public string OperationName
        {
            get
            {
                return this.operationName;
            }

            set
            {
                if (!Enum.TryParse(value, true, out this.name))
                {
                    throw new NotSupportedException();
                }

                this.operationName = value;
            }
        }

        [JsonIgnore]
        public IPath Path
        {
            get
            {
                if (null == this.path && !string.IsNullOrWhiteSpace(this.pathExpression))
                {
                    this.path = Microsoft.SCIM.Path.Create(this.pathExpression);
                }

                return this.path;
            }

            set
            {
                this.pathExpression = value?.ToString();
                this.path = value;
            }
        }

        public override string ToString()
        {
            string result =
                string.Format(
                    CultureInfo.InvariantCulture,
                    PatchOperation2Base.Template,
                    this.operationName,
                    this.pathExpression);
            return result;
        }
    }
}
