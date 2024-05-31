//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Net;
    using System.Text.Json.Serialization;

    public sealed class ErrorResponse : Schematized, IJsonOnDeserializing
    {
        private ErrorType errorType;

        [JsonPropertyName(ProtocolAttributeNames.ErrorType)]
        public string errorTypeValue;

        private Response response;

        public ErrorResponse()
        {
            this.Initialize();
            this.AddSchema(ProtocolSchemaIdentifiers.Version2Error);
        }

        [JsonPropertyName(ProtocolAttributeNames.Detail)]
        public string Detail { get; set; }

        public ErrorType ErrorType
        {
            get { return this.errorType; }

            set
            {
                this.errorType = value;
                this.errorTypeValue = Enum.GetName(typeof(ErrorType), value);
            }
        }

        public HttpStatusCode Status
        {
            get { return this.response.Status; }

            set { this.response.Status = value; }
        }

        private void Initialize()
        {
            this.response = new Response();
        }

        public new void OnDeserializing()
        {
            this.Initialize();
        }
    }
}
