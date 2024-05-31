//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public abstract class PatchRequest2Base<TOperation> : Schematized, IJsonOnDeserialized, IJsonOnDeserializing
        where TOperation : PatchOperation2Base
    {
        [JsonPropertyName(ProtocolAttributeNames.Operations), JsonPropertyOrder(2), JsonInclude]
        private List<TOperation> operationsValue;
        private IReadOnlyCollection<TOperation> operationsWrapper;

        public PatchRequest2Base()
        {
            this.OnInitialization();
            this.OnInitialized();
            this.AddSchema(ProtocolSchemaIdentifiers.Version2PatchOperation);
        }

        protected PatchRequest2Base(IReadOnlyCollection<TOperation> operations)
            : this()
        {
            this.operationsValue.AddRange(operations);
        }

        [JsonIgnore]
        public IReadOnlyCollection<TOperation> Operations
        {
            get
            {
                return this.operationsWrapper;
            }
        }

        public void AddOperation(TOperation operation)
        {
            ArgumentNullException.ThrowIfNull(operation);

            this.operationsValue.Add(operation);
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
            this.operationsValue = new List<TOperation>();
        }

        private void OnInitialized()
        {
            this.operationsWrapper = this.operationsValue.AsReadOnly();
        }
    }
}
