//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Text.Json.Serialization;
    public sealed class ExtensionAttributeEnterpriseUser2
    {
        [JsonPropertyName(AttributeNames.Manager), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Manager Manager
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.CostCenter), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string CostCenter
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Department), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Department
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Division), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Division
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.EmployeeNumber)]
        public string EmployeeNumber
        {
            get;
            set;
        }

        [JsonPropertyName(AttributeNames.Organization), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Organization
        {
            get;
            set;
        }
    }
}
