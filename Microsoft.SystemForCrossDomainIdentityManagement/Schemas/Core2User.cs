//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.SCIM
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text.Json.Serialization;

    public abstract class Core2User : UserBase, IJsonOnDeserializing
    {
        private IDictionary<string, IDictionary<string, object>> customExtension;

        protected Core2User()
        {
            this.AddSchema(SchemaIdentifiers.Core2User);
            this.Metadata =
                new Core2Metadata()
                {
                    ResourceType = Types.User
                };
            this.OnInitialization();
        }

        [JsonPropertyName(AttributeNames.Active), JsonConverter(typeof(BooleanConverter))]
        public virtual bool Active { get; set; }

        [JsonPropertyName(AttributeNames.Addresses), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual IEnumerable<Address> Addresses { get; set; }

        public virtual IReadOnlyDictionary<string, IDictionary<string, object>> CustomExtension
        {
            get { return new ReadOnlyDictionary<string, IDictionary<string, object>>(this.customExtension); }
        }

        [JsonPropertyName(AttributeNames.DisplayName), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual string DisplayName { get; set; }

        [JsonPropertyName(AttributeNames.ElectronicMailAddresses), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual IEnumerable<ElectronicMailAddress> ElectronicMailAddresses { get; set; }

        [JsonPropertyName(AttributeNames.Ims), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual IEnumerable<InstantMessaging> InstantMessagings { get; set; }

        [JsonPropertyName(AttributeNames.Locale), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual string Locale { get; set; }

        [JsonPropertyName(AttributeNames.Metadata)]
        public virtual Core2Metadata Metadata { get; set; }

        [JsonPropertyName(AttributeNames.Name), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual Name Name { get; set; }

        [JsonPropertyName(AttributeNames.Nickname), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual string Nickname { get; set; }

        [JsonPropertyName(AttributeNames.PhoneNumbers), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual IEnumerable<PhoneNumber> PhoneNumbers { get; set; }

        [JsonPropertyName(AttributeNames.PreferredLanguage), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual string PreferredLanguage { get; set; }

        [JsonPropertyName(AttributeNames.Roles), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual IEnumerable<Role> Roles { get; set; }

        [JsonPropertyName(AttributeNames.TimeZone), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual string TimeZone { get; set; }

        [JsonPropertyName(AttributeNames.Title), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual string Title { get; set; }

        [JsonPropertyName(AttributeNames.UserType), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual string UserType { get; set; }

        public virtual void AddCustomAttribute(string key, object value)
        {
            if
            (
                key != null
                && key.StartsWith(SchemaIdentifiers.PrefixExtension, StringComparison.OrdinalIgnoreCase)
                && !key.StartsWith(SchemaIdentifiers.Core2EnterpriseUser, StringComparison.OrdinalIgnoreCase)
                && value is Dictionary<string, object> nestedObject
            )
            {
                this.customExtension.Add(key, nestedObject);
            }
        }

        public new void OnDeserializing()
        {
            this.OnInitialization();
        }

        private void OnInitialization()
        {
            this.customExtension = new Dictionary<string, IDictionary<string, object>>();
        }

        public override Dictionary<string, object> ToJson()
        {
            Dictionary<string, object> result = base.ToJson();

            foreach (KeyValuePair<string, IDictionary<string, object>> entry in this.CustomExtension)
            {
                result.Add(entry.Key, entry.Value);
            }

            return result;
        }
    }
}
