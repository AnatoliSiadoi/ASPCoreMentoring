using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MVCPresentationLayer.Models.Category;
using MVCPresentationLayer.Models.Supplier;

namespace MVCPresentationLayer.Models.Product
{
    public class ProductViewModelUpdate
    {
        [Required]
        [Display(Name = "Product identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Product name cannot exceed 25 characters. ")]
        [Display(Name = "Product name")]
        public string ProductName { get; set; }

        [StringLength(25, ErrorMessage = "Quantity per unit cannot exceed 25 characters. ")]
        [Display(Name = "Quantity per unit")]
        public string QuantityPerUnit { get; set; }

        [Range(0, double.PositiveInfinity, ErrorMessage = "Unit price should be positive")]
        [Display(Name = "Unit price")]
        public decimal UnitPrice { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Units in stock should be positive")]
        [Display(Name = "Units in stock")]
        public short UnitsInStock { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Units on order should be positive")]
        [Display(Name = "Units on order")]
        public short UnitsOnOrder { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Reorder level should be positive")]
        [Display(Name = "Reorder level")]
        public short ReorderLevel { get; set; }

        [Required]
        [Display(Name = "Discontinued")]
        public bool Discontinued { get; set; }

        [Display(Name = "Product category")]
        public int CategoryId { get; set; }

        [Display(Name = "Product supplier")]
        public int SupplierId { get; set; }

        public IEnumerable<SupplierViewModel> SupplierList { get; set; }

        public IEnumerable<CategoryViewModel> CategoryList { get; set; }
    }
}
