using pocketbase_csharp_sdk.Models;
using PocketBaseClient.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm
{
    public abstract class ItemBase : BaseModel
    {
        private string? _CollectionId = null;
        [JsonPropertyName("collectionId")]
        public override string? CollectionId
        {
            get => _CollectionId ?? _Collection?.Id;
            set
            {
                _CollectionId = value;
                _Collection = null;
            }
        }

        private string? _CollectionName = null;
        [JsonPropertyName("collectionName")]
        public override string? CollectionName
        {
            get => _CollectionName ?? _Collection?.Name;
            set => _CollectionName = value;
        }

        private CollectionBase? _Collection = null;
        [JsonIgnore]
        public CollectionBase? Collection
        {
            get => _Collection ??= DataServiceBase.GetCollectionById(CollectionId);
            internal set => _Collection = value;
        }

        private void Load()
        {

        }
        protected T Get<T>(Func<T> func)
        {
            Load();
            return func();
        }
        protected void Set<T>(T? value, ref T? valueVar)
        {
            if (value == null && valueVar == null) return;
            if (valueVar == null || !valueVar.Equals(value))
                valueVar = value;
        }

        private ItemMetadata? _Metadata = null;
        public ItemMetadata Metadata() => _Metadata ??= new ItemMetadata(this);

        public bool IsLoaded()
            => Metadata().IsLoaded && !Metadata().IsTrash;

        public bool Validate(out List<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            var vc = new ValidationContext(this);
            return Validator.TryValidateObject(this, vc, validationResults, true);
        }
    }
}
