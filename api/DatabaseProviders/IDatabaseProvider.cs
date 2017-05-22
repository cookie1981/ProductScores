using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.DatabaseProviders
{
    public interface IDatabaseRepository<T>
    {
        Task<List<T>> FindAllAsync();
        Task<T> FindByIdAsync(string id);
        Task<T> FindByUniqueElementAsync(string elementName, string elementValue);
        Task<bool> InsertAsync(T thingToInsert);
        Task<bool> DeleteAsync(string id);
        Task<bool> UpdateAsync(T thingToUpdate);
    }
}