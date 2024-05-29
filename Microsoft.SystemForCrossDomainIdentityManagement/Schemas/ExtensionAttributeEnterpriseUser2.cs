//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class ExtensionAttributeEnterpriseUser2
    {
        [DataMember(Name = AttributeNames.Manager, IsRequired = false, EmitDefaultValue = false)]
        public Manager Manager
        {
            get;
            set;
        }

        [DataMember(Name = AttributeNames.CostCenter, IsRequired = false, EmitDefaultValue = false)]
        public string CostCenter
        {
            get;
            set;
        }

        [DataMember(Name = AttributeNames.Department, IsRequired = false, EmitDefaultValue = false)]
        public string Department
        {
            get;
            set;
        }

        [DataMember(Name = AttributeNames.Division, IsRequired = false, EmitDefaultValue = false)]
        public string Division
        {
            get;
            set;
        }

        [DataMember(Name = AttributeNames.EmployeeNumber, IsRequired = false, EmitDefaultValue = false)]
        public string EmployeeNumber
        {
            get;
            set;
        }

        [DataMember(Name = AttributeNames.Organization, IsRequired = false, EmitDefaultValue = false)]
        public string Organization
        {
            get;
            set;
        }
    }
}
