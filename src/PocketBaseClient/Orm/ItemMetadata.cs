namespace PocketBaseClient.Orm
{
    public class ItemMetadata
    {
        public ItemBase Item { get; private init; }

        public bool IsCreated => Item.Id != null;
        public bool IsLoaded => IsCreated && Item.Updated != null;

        public bool IsTrash { get; set; } = false;

        public ItemMetadata(ItemBase item)
        {
            Item = item;
        }
    }
}
