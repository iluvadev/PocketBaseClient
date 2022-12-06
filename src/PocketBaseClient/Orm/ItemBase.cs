using pocketbase_csharp_sdk.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm
{
    public abstract class ItemBase : BaseModel
    {
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


        private async Task LoadAsync(bool forceLoad = false)
        {
            if (Collection == null) return;
            if (!Metadata.IsCreated) return;
            if (IsLoaded() && !forceLoad) return;

            await Collection.FillFromPbAsync(this);
        }
        private void Load(bool forceLoad = false)
            => LoadAsync(forceLoad).Wait();

        protected T Get<T>(Func<T> func)
        {
            Load();
            return func();
        }
        protected void Set<T>(T value, ref T valueVar)
        {
            if (value == null && valueVar == null) return;
            if (valueVar == null || !valueVar.Equals(value))
            {
                valueVar = value;
                Metadata.HasLocalChanges = true;
            }
        }

        private ItemMetadata? _Metadata = null;
        public ItemMetadata Metadata => _Metadata ??= new ItemMetadata(this);

        public bool IsLoaded()
            => Metadata.IsLoaded && !Metadata.IsTrash;

        public bool HasLocalChanges()
            => Metadata.HasLocalChanges;

        public bool Validate(out List<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            var vc = new ValidationContext(this);
            return Validator.TryValidateObject(this, vc, validationResults, true);
        }

        public bool IsValid() => Validate(out _);

        public virtual void UpdateWith(ItemBase itemBase)
        {
            Created = itemBase.Created;
            Updated = itemBase.Updated;
        }
    }
}
