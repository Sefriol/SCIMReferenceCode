//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ElectronicMailAddress : TypedValue
    {
        internal ElectronicMailAddress()
        {
        }

        public const string Home = "home";
        public const string Other = "other";
        public const string Work = "work";
    }
}
