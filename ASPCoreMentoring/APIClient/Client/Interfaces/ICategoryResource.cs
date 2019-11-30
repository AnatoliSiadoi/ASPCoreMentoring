using APIClient.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIClient.Client.Interfaces
{
    public interface ICategoryResource
    {
        Task<IEnumerable<CategoryModel>> GetCategoriesList();
    }
}
