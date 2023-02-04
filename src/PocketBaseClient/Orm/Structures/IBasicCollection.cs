using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.Orm.Structures
{
    public interface IBasicCollection : IBasicList
    {
        /// <summary>
        /// Save Changes in the list
        /// </summary>
        /// <param name="mode">Says what to save</param>
        /// <returns></returns>
        Task<bool> SaveChangesAsync(ListSaveDiscardModes mode);

        /// <summary>
        /// Discard changes in list
        /// </summary>
        /// <param name="mode">Says what to discard</param>
        void DiscardChanges(ListSaveDiscardModes mode);
    }
}
