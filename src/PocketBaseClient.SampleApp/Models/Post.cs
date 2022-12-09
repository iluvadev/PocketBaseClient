
// This file was generated automatically on 8/12/2022 21:42:16(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
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

namespace PocketBaseClient.SampleApp.Models
{
    public partial class Post : ItemBase
    {
        #region Collection
        private static CollectionBase? _Collection = null;
        [JsonIgnore]
        public override CollectionBase Collection => _Collection ??= DataServiceBase.GetCollection<Post>()!;
        #endregion Collection

        #region Field Properties
        private string? _Title = null;
        [JsonPropertyName("title")]
        [PocketBaseField(id: "eqr6uyuh", name: "title", required: true, system: false, unique: true, type: "text")]
        [Required(ErrorMessage = @"title is required")]
        [StringLength(2147483647, MinimumLength = 5, ErrorMessage = "Minimum 5, Maximum 2147483647 characters")]
        public string? Title
        {
           get => Get(() => _Title);
           set => Set(value, ref _Title);
        }

        private Author? _Author = null;
        [JsonPropertyName("author")]
        [PocketBaseField(id: "cetvvrdy", name: "author", required: false, system: false, unique: false, type: "relation")]
        [JsonConverter(typeof(RelationConverter<Author>))]
        public Author? Author
        {
           get => Get(() => _Author);
           set => Set(value, ref _Author);
        }

        private string? _Summary = null;
        [JsonPropertyName("summary")]
        [PocketBaseField(id: "s9fvv8uu", name: "summary", required: false, system: false, unique: false, type: "text")]
        [StringLength(256, ErrorMessage = "Maximum 256 characters")]
        public string? Summary
        {
           get => Get(() => _Summary);
           set => Set(value, ref _Summary);
        }

        private string? _Content = null;
        [JsonPropertyName("content")]
        [PocketBaseField(id: "irhofrtf", name: "content", required: false, system: false, unique: false, type: "text")]
        public string? Content
        {
           get => Get(() => _Content);
           set => Set(value, ref _Content);
        }

        private DateTime? _Published = null;
        [JsonPropertyName("published")]
        [PocketBaseField(id: "3zouwrab", name: "published", required: false, system: false, unique: false, type: "date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Published
        {
           get => Get(() => _Published);
           set => Set(value, ref _Published);
        }

        private StatusEnum? _Status = null;
        [JsonPropertyName("status")]
        [PocketBaseField(id: "eusjau7x", name: "status", required: false, system: false, unique: false, type: "select")]
        [JsonConverter(typeof(EnumConverter<StatusEnum>))]
        public StatusEnum? Status
        {
           get => Get(() => _Status);
           set => Set(value, ref _Status);
        }

        private CategoriesList _Categories = new CategoriesList();
        [JsonPropertyName("categories")]
        [PocketBaseField(id: "2ftkqyzs", name: "categories", required: false, system: false, unique: false, type: "relation")]
        [JsonConverter(typeof(RelationListConverter<CategoriesList, Category>))]
        public CategoriesList Categories
        {
           get => Get(() => _Categories ??= new CategoriesList());
           private set => Set(value, ref _Categories);
        }

        private List<Tag> _Tags = new List<Tag>();
        [JsonPropertyName("tags")]
        [PocketBaseField(id: "vqnnjaiq", name: "tags", required: false, system: false, unique: false, type: "relation")]
        [JsonConverter(typeof(RelationListConverter<List<Tag>, Tag>))]
        public List<Tag> Tags
        {
           get => Get(() => _Tags ??= new List<Tag>());
           private set => Set(value, ref _Tags);
        }


        #endregion Field Properties

        public override void UpdateWith(ItemBase itemBase)
        {
            base.UpdateWith(itemBase);

            if (itemBase is Post item)
            {
                Title = item.Title;
                Author = item.Author;
                Summary = item.Summary;
                Content = item.Content;
                Published = item.Published;
                Status = item.Status;
                Categories = item.Categories;
                Tags = item.Tags;

            }
        }

        public override string ToString()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(this, options);
        }

        protected override IEnumerable<ItemBase?> RelatedItems 
            => base.RelatedItems.Union(new List<ItemBase?>() { Author }).Union(Categories).Union(Tags);

        #region Collection
        public static CollectionPosts GetCollection() 
            => (CollectionPosts)DataServiceBase.GetCollection<Post>()!;
        #endregion Collection


        #region GetById
        public static Post? GetById(string id, bool reload = false) 
            => GetByIdAsync(id, reload).Result;

        public static async Task<Post?> GetByIdAsync(string id, bool reload = false)
            => await DataServiceBase.GetCollection<Post>()!.GetByIdAsync(id, reload);
        #endregion GetById
    }
}
