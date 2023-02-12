
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
    public partial class Post : ItemBase
    {
        #region Collection
        private static CollectionBase? _Collection = null;
        /// <inheritdoc />
        [JsonIgnore]
        public override CollectionBase Collection => _Collection ??= DataServiceBase.GetCollection<Post>()!;
        #endregion Collection

        #region Field Properties
        private string? _Title = default;
        /// <summary> Maps to 'title' field in PocketBase </summary>
        [JsonPropertyName("title")]
        [PocketBaseField(id: "eqr6uyuh", name: "title", required: true, system: false, unique: true, type: "text")]
        [Display(Name = "Title")]
        [Required(ErrorMessage = @"Title is required")]
        [StringLength(2147483647, MinimumLength = 5, ErrorMessage = "Minimum 5, Maximum 2147483647 characters")]
        public string Title { get => Get(() => _Title ??= string.Empty); set => Set(value, ref _Title); }

        private Author? _Author = default;
        /// <summary> Maps to 'author' field in PocketBase </summary>
        [JsonPropertyName("author")]
        [PocketBaseField(id: "cetvvrdy", name: "author", required: false, system: false, unique: false, type: "relation")]
        [Display(Name = "Author")]
        [JsonConverter(typeof(RelationConverter<Author>))]
        public Author? Author { get => Get(() => _Author ??= default); set => Set(value, ref _Author); }

        private string? _Summary = default;
        /// <summary> Maps to 'summary' field in PocketBase </summary>
        [JsonPropertyName("summary")]
        [PocketBaseField(id: "s9fvv8uu", name: "summary", required: false, system: false, unique: false, type: "text")]
        [Display(Name = "Summary")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters")]
        public string? Summary { get => Get(() => _Summary ??= default); set => Set(value, ref _Summary); }

        private string? _Content = default;
        /// <summary> Maps to 'content' field in PocketBase </summary>
        [JsonPropertyName("content")]
        [PocketBaseField(id: "irhofrtf", name: "content", required: false, system: false, unique: false, type: "text")]
        [Display(Name = "Content")]
        public string? Content { get => Get(() => _Content ??= default); set => Set(value, ref _Content); }

        private DateTime? _Published = default;
        /// <summary> Maps to 'published' field in PocketBase </summary>
        [JsonPropertyName("published")]
        [PocketBaseField(id: "3zouwrab", name: "published", required: false, system: false, unique: false, type: "date")]
        [Display(Name = "Published")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Published { get => Get(() => _Published ??= default); set => Set(value, ref _Published); }

        private StatusEnum? _Status = default;
        /// <summary> Maps to 'status' field in PocketBase </summary>
        [JsonPropertyName("status")]
        [PocketBaseField(id: "eusjau7x", name: "status", required: false, system: false, unique: false, type: "select")]
        [Display(Name = "Status")]
        [JsonConverter(typeof(EnumConverter<StatusEnum>))]
        public StatusEnum? Status { get => Get(() => _Status ??= default); set => Set(value, ref _Status); }

        private CategoriesList? _Categories = default;
        /// <summary> Maps to 'categories' field in PocketBase </summary>
        [JsonPropertyName("categories")]
        [PocketBaseField(id: "2ftkqyzs", name: "categories", required: false, system: false, unique: false, type: "relation")]
        [Display(Name = "Categories")]
        [JsonInclude]
        [JsonConverter(typeof(RelationListConverter<CategoriesList, Category>))]
        public CategoriesList Categories { get => Get(() => _Categories ??= new CategoriesList(this)); private set => Set(value, ref _Categories); }

        private TagsList? _Tags = default;
        /// <summary> Maps to 'tags' field in PocketBase </summary>
        [JsonPropertyName("tags")]
        [PocketBaseField(id: "vqnnjaiq", name: "tags", required: false, system: false, unique: false, type: "relation")]
        [Display(Name = "Tags")]
        [JsonInclude]
        [JsonConverter(typeof(RelationListConverter<TagsList, Tag>))]
        public TagsList Tags { get => Get(() => _Tags ??= new TagsList(this)); private set => Set(value, ref _Tags); }

        #endregion Field Properties

        /// <inheritdoc />
        public override void UpdateWith(ItemBase itemBase)
        {
            // Do not Update with this instance
            if (ReferenceEquals(this, itemBase)) return;

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

        #region Constructors

        public Post() : base()
        {
        }

        [JsonConstructor]
        public Post(string? id, DateTime? created, DateTime? updated, string @title, Author? @author, string? @summary, string? @content, DateTime? @published, StatusEnum? @status, CategoriesList @categories, TagsList @tags)
            : base(id, created, updated)
        {
            this.Title = @title;
            this.Author = @author;
            this.Summary = @summary;
            this.Content = @content;
            this.Published = @published;
            this.Status = @status;
            this.Categories = @categories;
            this.Tags = @tags;

            AddInternal(this);
        }
        #endregion

        /// <inheritdoc />
        protected override IEnumerable<ItemBase?> RelatedItems 
            => base.RelatedItems.Union(new List<ItemBase?>() { Author }).Union(Categories).Union(Tags);

        #region Collection
        public static CollectionPosts GetCollection() 
            => (CollectionPosts)DataServiceBase.GetCollection<Post>()!;
        #endregion Collection

        public static async Task<Post?> GetByIdAsync(string id, bool reload = false)
            => await GetCollection().GetByIdAsync(id, reload);

        public static Post? GetById(string id, bool reload = false)
            => GetCollection().GetById(id, reload);
    }
}
