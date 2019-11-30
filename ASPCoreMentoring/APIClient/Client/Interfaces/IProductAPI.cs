using APIClient.Client.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIClient.Client.Interfaces
{
    public interface IProductAPI
    {
        [Get("ProductAPI")]
        Task<IEnumerable<ProductModel>> GetProductsList();
    }
}
