using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObject;

namespace BusinessLayer.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDTO>> GetAllSuppliers();
    }
}
