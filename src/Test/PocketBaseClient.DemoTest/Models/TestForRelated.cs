
// This file was generated automatically for the PocketBase Application demo-test (https://orm-csharp-test.pockethost.io)
//    See CodeGenerationSummary.txt for more details
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk.Json;
using PocketBaseClient.Orm;
using PocketBaseClient.Orm.Attributes;
using PocketBaseClient.Orm.Json;
using PocketBaseClient.Orm.Validators;
using PocketBaseClient.Services;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocketBaseClient.DemoTest.Models
{
    public partial class TestForRelated : ItemBase
    {
        #region Collection
        private static CollectionBase? _Collection = null;
        /// <inheritdoc />
        [JsonIgnore]
        public override CollectionBase Collection => _Collection ??= DataServiceBase.GetCollection<TestForRelated>()!;
        #endregion Collection

        #region Field Properties
        private float? _NumberUnique = null;
        /// <summary> Maps to 'number_unique' field in PocketBase </summary>
        [JsonPropertyName("number_unique")]
        [PocketBaseField(id: "s10g39sb", name: "number_unique", required: false, system: false, unique: true, type: "number")]
        [Display(Name = "Number unique")]
        public float? NumberUnique { get => Get(() => _NumberUnique); set => Set(value, ref _NumberUnique); }

        private float? _NumberNonempty = null;
        /// <summary> Maps to 'number_nonempty' field in PocketBase </summary>
        [JsonPropertyName("number_nonempty")]
        [PocketBaseField(id: "iy8rrkm2", name: "number_nonempty", required: true, system: false, unique: false, type: "number")]
        [Display(Name = "Number nonempty")]
        [Required(ErrorMessage = @"NumberNonempty is required")]
        public float? NumberNonempty { get => Get(() => _NumberNonempty); set => Set(value, ref _NumberNonempty); }

        private float? _NumberNonemptyUnique = null;
        /// <summary> Maps to 'number_nonempty_unique' field in PocketBase </summary>
        [JsonPropertyName("number_nonempty_unique")]
        [PocketBaseField(id: "mmzxqln4", name: "number_nonempty_unique", required: true, system: false, unique: true, type: "number")]
        [Display(Name = "Number nonempty unique")]
        [Required(ErrorMessage = @"NumberNonemptyUnique is required")]
        public float? NumberNonemptyUnique { get => Get(() => _NumberNonemptyUnique); set => Set(value, ref _NumberNonemptyUnique); }

        #endregion Field Properties

        /// <inheritdoc />
        public override void UpdateWith(ItemBase itemBase)
        {
            // Do not Update with this instance
            if (ReferenceEquals(this, itemBase)) return;

            base.UpdateWith(itemBase);

            if (itemBase is TestForRelated item)
            {
                NumberUnique = item.NumberUnique;
                NumberNonempty = item.NumberNonempty;
                NumberNonemptyUnique = item.NumberNonemptyUnique;

            }
        }

        #region Constructors

        public TestForRelated() : base()
        {
        }

        [JsonConstructor]
        public TestForRelated(string? id, DateTime? created, DateTime? updated, float? @numberUnique, float? @numberNonempty, float? @numberNonemptyUnique)
            : base(id, created, updated)
        {
            this.NumberUnique = @numberUnique;
            this.NumberNonempty = @numberNonempty;
            this.NumberNonemptyUnique = @numberNonemptyUnique;

            AddInternal(this);
        }
        #endregion

        #region Collection
        public static CollectionTestForRelateds GetCollection() 
            => (CollectionTestForRelateds)DataServiceBase.GetCollection<TestForRelated>()!;
        #endregion Collection

        public static async Task<TestForRelated?> GetByIdAsync(string id, bool reload = false)
            => await GetCollection().GetByIdAsync(id, reload);

        public static TestForRelated? GetById(string id, bool reload = false)
            => GetCollection().GetById(id, reload);
    }
}
