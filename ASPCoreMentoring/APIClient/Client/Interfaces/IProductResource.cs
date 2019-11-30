using APIClient.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIClient.Client.Interfaces
{
    public interface IProductResource
    {
        Task<IEnumerable<ProductModel>> GetProductsList();
    }
}
