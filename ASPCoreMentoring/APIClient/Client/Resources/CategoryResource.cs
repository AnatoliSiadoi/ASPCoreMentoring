using APIClient.Client.Interfaces;
using APIClient.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIClient.Client.Resources
{
    internal class CategoryResource : ResourceGatewayBase<ICategoryAPI>, ICategoryResource
    {
        public CategoryResource(ICategoryAPI categoryAPI)
            : base(categoryAPI)
        {
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoriesList()
        {
            var result = await ApiClient.GetCategoriesList();
            return result;
        }
    }
}
