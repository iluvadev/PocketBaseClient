using PocketBaseClient.Orm;
using PocketBaseClient.Services;

namespace PocketBaseClient
{
    public static class RelationListExtensions
    {
        public static void UpdateWith<T>(this IList<T> list, IList<T> listWithChanges)
            where T : ItemBase, new()
        {
            List<string> updatedIds = new List<string>();
            foreach (var itemWithChanges in listWithChanges)
            {
                var itemToUpdate = list.FirstOrDefault(i => i.Id == itemWithChanges.Id);
                if (itemToUpdate != null)
                    itemToUpdate.UpdateWith(itemWithChanges);
                else
                {
                    var itemToInsert = DataServiceBase.UpdateCached(itemWithChanges);
                    list.Add(itemToInsert);
                }
                updatedIds.Add(itemWithChanges.Id!);
            }

            var elemsToRemove = list.Where(i => i.Metadata.IsCreated && !updatedIds.Contains(i.Id!)).ToList();
            foreach(var elemToRemove in elemsToRemove)
                list.Remove(elemToRemove);
        }
    }
}
