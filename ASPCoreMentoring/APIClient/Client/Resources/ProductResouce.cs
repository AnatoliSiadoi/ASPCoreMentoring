using APIClient.Client.Interfaces;
using APIClient.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIClient.Client.Resources
{
    internal class ProductResource : ResourceGatewayBase<IProductAPI>, IProductResource
    {
        public ProductResource(IProductAPI productAPI)
            : base(productAPI)
        {
        }

        public async Task<IEnumerable<ProductModel>> GetProductsList()
        {
            var result = await ApiClient.GetProductsList();
            return result;
        }
    }
}
