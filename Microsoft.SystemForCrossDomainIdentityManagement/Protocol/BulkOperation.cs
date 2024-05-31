//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Net.Http;
    using System.Text.Json.Serialization;
    public abstract class BulkOperation
    {
        private HttpMethod method;
        private string methodName;

        public BulkOperation()
        {
            this.Identifier = Guid.NewGuid().ToString();
        }

        protected BulkOperation(string identifier)
        {
            this.Identifier = identifier;
        }

        [JsonPropertyName(ProtocolAttributeNames.BulkOperationIdentifier), JsonPropertyOrder(1)]
        public string Identifier
        {
            get;
            private set;
        }

        public HttpMethod Method
        {
            get => this.method;

            set
            {
                this.method = value;
                if (value != null)
                {
                    this.methodName = value.ToString();
                }
            }
        }

        [JsonPropertyName(ProtocolAttributeNames.Method), JsonPropertyOrder(0)]
        public string MethodName
        {
            get => this.methodName;

            set
            {
                this.method = new HttpMethod(value);
                this.methodName = value;
            }
        }
    }
}
