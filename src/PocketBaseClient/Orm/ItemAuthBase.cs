// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk.Models;
using PocketBaseClient.Orm.Json;
using System;
using System.Net.Mail;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace PocketBaseClient.Orm
{
    public abstract class ItemAuthBase : ItemBase, IBaseAuthModel
    {
        /// <summary> Maps to 'email' field in PocketBase </summary>
        [JsonPropertyName("email")]
        [JsonConverter(typeof(EmailConverter))]
        [JsonInclude]
        public MailAddress? Email { get; private set; }
        string? IBaseAuthModel.Email { get => this.Email?.Address; }

        /// <summary> Maps to 'emailVisibility' field in PocketBase </summary>
        [JsonPropertyName("emailVisibility")]
        [JsonInclude]
        public bool? EmailVisibility { get; private set; }

        /// <summary> Maps to 'username' field in PocketBase </summary>
        [JsonPropertyName("username")]
        [JsonInclude]
        public string? Username { get; private set; }

        /// <summary> Maps to 'verified' field in PocketBase </summary>
        [JsonPropertyName("verified")]
        [JsonInclude]
        public bool? Verified { get; private set; }

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        public ItemAuthBase() : base() { }

        [JsonConstructor]
        public ItemAuthBase(string? id, DateTime? created, DateTime? updated, MailAddress? email, bool? emailVisibility, string? username, bool? verified) 
            : base(id, created, updated)
        {
            Email = email;
            EmailVisibility = emailVisibility;
            Username = username;
            Verified = verified;
        }
        #endregion Ctor


        /// <summary>
        /// Update the object with data from other
        /// </summary>
        /// <param name="itemBase"></param>
        public override void UpdateWith(ItemBase itemBase)
        {
            // Do not Update with this instance
            if (ReferenceEquals(this, itemBase)) return;

            base.UpdateWith(itemBase);

            if (itemBase is ItemAuthBase item)
            {
                Email = item.Email;
                EmailVisibility = item.EmailVisibility;
                Username = item.Username;
                Verified = item.Verified;
            }
        }
    }
}
