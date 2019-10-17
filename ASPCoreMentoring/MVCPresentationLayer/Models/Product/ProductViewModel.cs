using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Models.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Product name")]
        public string ProductName { get; set; }

        [Display(Name = "Quantity per unit")]
        public string QuantityPerUnit { get; set; }

        [Display(Name = "Unit price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Units in stock")]
        public short UnitsInStock { get; set; }

        [Display(Name = "Units on order")]
        public short UnitsOnOrder { get; set; }

        [Display(Name = "Reorder level")]
        public short ReorderLevel { get; set; }

        [Display(Name = "Discontinued")]
        public bool Discontinued { get; set; }

        [Display(Name = "Supplier name")]
        public string SupplierName { get; set; }

        [Display(Name = "Category name")]
        public string CategoryName { get; set; }
    }
}
