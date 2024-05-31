//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Microsoft.SCIM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class BulkOperations<TOperation> : Schematized where TOperation : BulkOperation
    {
        [JsonPropertyName(ProtocolAttributeNames.Operations), JsonPropertyOrder(2)]
        public List<TOperation> operations;

        private IReadOnlyCollection<TOperation> operationsWrapper;

        private object thisLock;

        protected BulkOperations(string schemaIdentifier)
        {
            if (string.IsNullOrWhiteSpace(schemaIdentifier))
            {
                throw new ArgumentNullException(nameof(schemaIdentifier));
            }

            this.AddSchema(schemaIdentifier);
            this.OnInitialization();
            this.OnInitialized();
        }

        public IReadOnlyCollection<TOperation> Operations => this.operationsWrapper;

        public void AddOperation(TOperation operation)
        {
            ArgumentNullException.ThrowIfNull(operation);

            if (string.IsNullOrWhiteSpace(operation.Identifier))
            {
                throw new ArgumentException(
                    SystemForCrossDomainIdentityManagementProtocolResources.ExceptionUnidentifiableOperation);
            }

            bool Contains() => this.operations.Any((BulkOperation item) => string.Equals(item.Identifier,
                operation.Identifier,
                StringComparison.OrdinalIgnoreCase));

            if (!Contains())
            {
                lock (this.thisLock)
                {
                    if (!Contains())
                    {
                        this.operations.Add(operation);
                    }
                }
            }
        }

        private void OnInitialization()
        {
            this.thisLock = new object();
            this.operations = new List<TOperation>();
        }

        private void OnInitialized() => this.operationsWrapper = this.operations.AsReadOnly();
    }
}
