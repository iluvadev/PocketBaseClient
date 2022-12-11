// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk.Json;
using pocketbase_csharp_sdk.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm
{
    public abstract class ItemBase : BaseModel
    {
        #region Field Properties
        [JsonPropertyName("id")]
        [JsonInclude]
        public new string? Id { get; internal set; }

        [JsonPropertyName("collectionId")]
        [JsonInclude]
        public new string? CollectionId => Collection.Id;

        [JsonPropertyName("collectionName")]
        [JsonInclude]
        public new string? CollectionName => Collection.Name;

        [JsonIgnore]
        public abstract CollectionBase Collection { get; }

        [JsonPropertyName("created")]
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonInclude]
        public new DateTime? Created { get; private set; }

        [JsonPropertyName("updated")]
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonInclude]
        public new DateTime? Updated { get; private set; }
        #endregion Field Properties

        #region Get and Set 
        protected T Get<T>(Func<T> func)
        {
            Load();
            return func();
        }
        protected void Set<T>(T value, ref T valueVar)
        {
            if (value == null && valueVar == null) return;

            //if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(IList<>) &&
            //    typeof(T).GetGenericArguments()[0].BaseType == typeof(ItemBase))
            //{
            //    //IList<T> where T : ItemBase
            //}
            if (valueVar == null || !valueVar.Equals(value))
            {
                valueVar = value;
                Metadata_.HasLocalChanges = true;
            }
        }

        #endregion Get and Set 

        #region Metadata
        private ItemMetadata? _Metadata_ = null;
        [JsonIgnore]
        public ItemMetadata Metadata_ => _Metadata_ ??= new ItemMetadata(this);
        #endregion Metadata

        #region Validation
        public bool Validate(out List<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            var vc = new ValidationContext(this);
            return Validator.TryValidateObject(this, vc, validationResults, true);
        }

        public bool IsValid() => Validate(out _);
        #endregion Validation

        #region Load
        private async Task LoadAsync(bool forceLoad = false)
        {
            if (Collection == null) return;
            if (Metadata_.IsNew) return;
            if (Metadata_.IsTrash) return;
            if (Metadata_.IsLoaded && !forceLoad) return;

            if (!await Collection.FillFromPbAsync(this))
            {
                //IEPA!!
                // The registry does not exists in PocketBase
                Metadata_.IsTrash = true;
                throw new Exception($"Object does not exists in PocketBase; Collection:{Collection.Name}; RegistryId:{Id}");
            }
        }
        private void Load(bool forceLoad = false)
            => LoadAsync(forceLoad).Wait();
        #endregion Load

        #region Reload
        public async Task ReloadAsync() => await LoadAsync(true);
        public void Reload() => ReloadAsync().Wait();
        #endregion Reload

        #region Delete
        public bool Delete() => DeleteAsync().Result;
        public async Task<bool> DeleteAsync() => await Collection.DeleteAsync(this);
        #endregion Delete

        #region DiscardChanges
        public void DiscardChanges()
        {
            if (Metadata_.HasLocalChanges)
                Metadata_.SetNeedBeLoaded();

            if (Metadata_.IsNew)
                Metadata_.IsTrash = true;
        }
        #endregion DiscardChanges

        #region Save
        public bool Save(bool onlyIfChanges = false) => SaveAsync(onlyIfChanges).Result;
        public async Task<bool> SaveAsync(bool onlyIfChanges = false) => await Collection.SaveAsync(this, onlyIfChanges);
        #endregion Save

        public bool IsSame(ItemBase item)
            => item.CollectionId == CollectionId && item.Id == Id;

        protected internal virtual IEnumerable<ItemBase?> RelatedItems => Enumerable.Empty<ItemBase>();

        public virtual void UpdateWith(ItemBase itemBase)
        {
            Created = itemBase.Created;
            Updated = itemBase.Updated;
        }

        public ItemBase()
        {
            Id = Random.Shared.PseudorandomString(15).ToLowerInvariant();
            Collection.AddToCache(this);
        }

        public override string ToString()
        {
            return $"{GetType().Name}.{Id}";
        }
    }
}
