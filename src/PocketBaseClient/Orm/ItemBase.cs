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
using PocketBaseClient.Orm.Structures;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm
{
    /// <summary>
    /// Base class for a mapped registry
    /// </summary>
    public abstract class ItemBase : BaseModel
    {
        #region Field Properties
        private string? _Id = null;
        /// <summary> Maps to 'id' field in PocketBase </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public new string? Id
        {
            get => _Id ?? "";
            internal set
            {
                var oldValue = _Id;
                _Id = value;

                if (oldValue == null)
                {
                    // New Id: Add item to Collection 
                    Collection.AddInternal(this);
                }
                else if (value != oldValue)
                {
                    // Changing Id: Replace item Id in the Collection
                    Collection.ChangeIdInCache(oldValue, this);
                }
            }
        }

        /// <summary> Maps to 'collectionId' field in PocketBase </summary>
        [JsonPropertyName("collectionId")]
        [JsonInclude]
        public new string? CollectionId => Collection.Id;

        /// <summary> Maps to 'collectionName' field in PocketBase </summary>
        [JsonPropertyName("collectionName")]
        [JsonInclude]
        public new string? CollectionName => Collection.Name;

        /// <summary> The Collection where the Item belongs to </summary>
        [JsonIgnore]
        public abstract CollectionBase Collection { get; }

        /// <summary> Maps to 'created' field in PocketBase </summary>
        [JsonPropertyName("created")]
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonInclude]
        public new DateTime? Created { get; private set; }

        /// <summary> Maps to 'updated' field in PocketBase </summary>
        [JsonPropertyName("updated")]
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonInclude]
        public new DateTime? Updated { get; private set; }
        #endregion Field Properties

        #region Get and Set 
        protected T Get<T>(Func<T> func)
        {
            Load();
            T value = func();
            if (value is IOwnedByItem ownedBy)
                ownedBy.Owner = this;

            return value;
        }
        protected void Set<T>(T value, ref T valueVar)
        {
            if (valueVar is IOwnedByItem ownedBy)
                ownedBy.Owner = this;

            if (valueVar is IBasicList basicList && value is IBasicList valueBasicList)
            {
                basicList.UpdateWith(valueBasicList);
                SetModified();
            }
            else
            {
                if (value == null && valueVar == null) return;

                if (valueVar == null || !valueVar.Equals(value))
                {
                    valueVar = value;
                    SetModified();
                }
            }
        }
        internal bool SetModified(bool modification = true)
            => Metadata_.HasLocalChanges |= modification;

        #endregion Get and Set 

        #region Metadata
        private ItemMetadata? _Metadata_ = null;
        /// <summary>
        /// The Metadata information about the object and mapping to PocketBase
        /// </summary>
        [JsonIgnore]
        public ItemMetadata Metadata_
        {
            get => _Metadata_ ??= new ItemMetadata(this);
            private set => _Metadata_ = value;
        }
        #endregion Metadata

        #region Validation
        /// <summary>
        /// Validate the Object data with PocketBase definitions
        /// </summary>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public bool Validate(out List<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            var vc = new ValidationContext(this);
            return Validator.TryValidateObject(this, vc, validationResults, true);
        }
        internal bool IsValid() => Validate(out _);
        #endregion Validation

        #region Load
        private async Task LoadAsync(bool forceLoad = false)
        {
            if (NeedToBeLoadedFromPb(forceLoad))
                await LoadFromPbAsync();
        }

        private void Load(bool forceLoad = false)
        {
            if (NeedToBeLoadedFromPb(forceLoad))
                LoadFromPb();
        }


        private bool NeedToBeLoadedFromPb(bool forceLoad = false)
        {
            if (Collection == null) return false;
            if (Metadata_.IsNew) return false;
            if (Metadata_.IsTrash) return false;
            if (Metadata_.IsLoaded && !forceLoad) return false;
            return true;
        }
        private void LoadFromPb()
        {
            if (!Collection.FillFromPb(this))
            {
                // TODO: Need a review

                // The registry does not exists in PocketBase
                Metadata_.IsTrash = true;
                throw new Exception($"Object does not exists in PocketBase; Collection:{Collection.Name}; RegistryId:{Id}");
            }
        }
        private async Task LoadFromPbAsync()
        {
            if (!await Collection.FillFromPbAsync(this))
            {
                // The registry does not exists in PocketBase
                Metadata_.IsTrash = true;
                throw new Exception($"Object does not exists in PocketBase; Collection:{Collection.Name}; RegistryId:{Id}");
            }
        }

        #endregion Load

        #region Reload
        /// <summary>
        /// Reloads the object with the data stored in PocketBase (async)
        /// </summary>
        /// <returns></returns>
        public async Task ReloadAsync() => await LoadAsync(true);
        
        /// <summary>
        /// Reloads the object with the data stored in PocketBase (async)
        /// </summary>
        /// <returns></returns>
        public void Reload() => Load(true);
        #endregion Reload

        #region Delete
        /// <summary>
        /// Deletes the object in PocketBase
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            if (Metadata_.IsTrash) return false;
            if (Metadata_.IsNew)
                Metadata_.IsTrash = true;
            else
                Metadata_.SyncStatus = ItemSyncStatuses.ToBeDeleted;
            return true;
        }

        #endregion Delete

        #region DiscardChanges
        /// <summary>
        /// Discards all changes of the object that are not saved in PocketBase
        /// </summary>
        public void DiscardChanges()
        {
            if (Metadata_.HasLocalChanges)
                Metadata_.SetNeedBeLoaded();

            if (Metadata_.IsNew)
                Metadata_.IsTrash = true;
        }
        #endregion DiscardChanges

        #region Save

        /// <summary>
        /// Saves the object to PocketBase (internally performs insert or update) (async)
        /// </summary>
        /// <param name="onlyIfChanges">False to force saving the object also if is unmodified (default behaviour)</param>
        /// <returns></returns>
        public async Task<bool> SaveAsync(bool onlyIfChanges = false)
            => await Collection.SaveAsync(this, onlyIfChanges);

        /// <summary>
        /// Saves the object to PocketBase (internally performs insert or update)
        /// </summary>
        /// <param name="onlyIfChanges">False to force saving the object also if is unmodified (default behaviour)</param>
        /// <returns></returns>
        public bool Save(bool onlyIfChanges = false)
            => Collection.Save(this, onlyIfChanges);
        #endregion Save

        /// <summary>
        /// Indicates if two objects refers to the same PocketBase registry
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool IsSame(BaseModel? item)
            => item != null && item.CollectionId == CollectionId && item.Id == Id;

        protected internal virtual IEnumerable<ItemBase?> RelatedItems
            => Enumerable.Empty<ItemBase>();

        protected internal virtual IEnumerable<FieldFileBase?> RelatedFiles
            => Enumerable.Empty<FieldFileBase>();

        /// <summary>
        /// Update the object with data from other
        /// </summary>
        /// <param name="itemBase"></param>
        public virtual void UpdateWith(ItemBase itemBase)
        {
            // Do not Update with this instance
            if (ReferenceEquals(this, itemBase)) return;

            // Update metadata
            bool isErased = Metadata_.IsTobeDeleted;
            Metadata_ = itemBase.Metadata_;
            if (isErased)
                Metadata_.SyncStatus = ItemSyncStatuses.ToBeDeleted;

            // Update fields
            Created = itemBase.Created;
            Updated = itemBase.Updated;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public ItemBase()
        {
            Id = Random.Shared.PseudorandomString(15).ToLowerInvariant();
        }

        [JsonConstructor]
        public ItemBase(string? id, DateTime? created, DateTime? updated)
        {
            _Id = id;
            Created = created;
            Updated = updated;
            Metadata_.SetLoaded();
        }

        protected object? AddInternal(object? element) => Collection.AddInternal(element);

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{GetType().Name}.{Id}";
        }
    }
}
