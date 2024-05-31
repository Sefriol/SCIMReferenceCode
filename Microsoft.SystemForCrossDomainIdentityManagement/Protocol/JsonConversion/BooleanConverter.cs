namespace Microsoft.SCIM
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// SCIM specification does not allow strings to be represented as boolean values,
    /// but this seems to be a common issue with Azure Entra
    /// Details: https://stackoverflow.com/questions/59264809/azure-user-group-provisioning-with-scim-problem-with-boolean-values
    /// https://datatracker.ietf.org/doc/html/rfc7159#section-3
    /// https://learn.microsoft.com/en-us/entra/identity/app-provisioning/application-provisioning-config-problem-scim-compatibility
    /// </summary>
    public class BooleanConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.True:
                    return true;
                case JsonTokenType.False:
                    return false;
                case JsonTokenType.String:
                    return reader.GetString().ToUpperInvariant() switch
                    {
                        "TRUE" => true,
                        "FALSE" => false,
                        _ => throw new JsonException()
                    };
                default:
                    throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteBooleanValue(value);
        }
    }
}
