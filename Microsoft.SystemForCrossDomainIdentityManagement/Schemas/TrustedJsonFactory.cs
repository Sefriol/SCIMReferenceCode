//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System.Collections.Generic;

    public class TrustedJsonFactory : JsonFactory
    {
        public override Dictionary<string, object> Create(string json)
        {
            Dictionary<string, object> result =
                System.Text.Json.JsonSerializer.Deserialize<Dictionary<string,object>>(
                    json);
            return result;
        }

        public override string Create(string[] json)
        {
            string result = System.Text.Json.JsonSerializer.Serialize(json);
            return result;
        }

        public override string Create(Dictionary<string, object> json)
        {
            string result = System.Text.Json.JsonSerializer.Serialize(json);
            return result;
        }

        public override string Create(IDictionary<string, object> json)
        {
            string result = System.Text.Json.JsonSerializer.Serialize(json);
            return result;
        }

        public override string Create(IReadOnlyDictionary<string, object> json)
        {
            string result = System.Text.Json.JsonSerializer.Serialize(json);
            return result;
        }
    }
}
