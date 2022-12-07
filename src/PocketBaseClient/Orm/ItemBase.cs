using pocketbase_csharp_sdk.Json;
using pocketbase_csharp_sdk.Models;
using System.ComponentModel.DataAnnotations;
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
                Metadata.HasLocalChanges = true;
            }
        }

        #endregion Get and Set 

        #region Metadata
        private ItemMetadata? _Metadata = null;
        public ItemMetadata Metadata => _Metadata ??= new ItemMetadata(this);
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
            if (Metadata.IsNew) return;
            if (Metadata.IsTrash) return;
            if (Metadata.IsLoaded && !forceLoad) return;

            if(!await Collection.FillFromPbAsync(this))
            {
                //IEPA!!
                // The registry does not exists in PocketBase
                Metadata.IsTrash = true;
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
            if (Metadata.HasLocalChanges)
                Metadata.SetNeedBeLoaded();
        }
        #endregion DiscardChanges

        #region Save
        public bool Save() => SaveAsync().Result;
        public async Task<bool> SaveAsync() => await Collection.SaveAsync(this);
        #endregion Save

        public virtual void UpdateWith(ItemBase itemBase)
        {
            Created = itemBase.Created;
            Updated = itemBase.Updated;
        }

        public ItemBase()
        {
            Id = Random.Shared.PseudorandomString(15).ToLowerInvariant();
        }
    }
}
