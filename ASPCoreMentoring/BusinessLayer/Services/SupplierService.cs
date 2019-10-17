using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObject;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IRepository<SupplierEntity> _supplierRepository;

        public SupplierService(IRepository<SupplierEntity> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<IEnumerable<SupplierDTO>> GetAllSuppliers()
        {
            IEnumerable<SupplierDTO> result = null;
            var allSupplierList = await _supplierRepository.GetAllQueryable().ToListAsync();
            if (allSupplierList.Any())
            {
                result = allSupplierList.Select(ConvertSupplierEntityToDTO);
            }

            return result;
        }

        private SupplierDTO ConvertSupplierEntityToDTO(SupplierEntity inputEntity)
        {
            if (inputEntity == null)
                return null;

            return new SupplierDTO
            {
                Id = inputEntity.Id,
                CompanyName = inputEntity.CompanyName
            };
        }
    }
}
