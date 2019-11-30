using System.ComponentModel.DataAnnotations;

namespace MVCPresentationLayer.Models.Product
{
    public class ProductModel
    {

        [Required]
        [StringLength(25, ErrorMessage = "Product name cannot exceed 25 characters. ")]
        public string ProductName { get; set; }

        [StringLength(25, ErrorMessage = "Quantity per unit cannot exceed 25 characters. ")]
        public string QuantityPerUnit { get; set; }

        [Range(0, double.PositiveInfinity, ErrorMessage = "Unit price should be positive")]
        public decimal UnitPrice { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Units in stock should be positive")]
        public short UnitsInStock { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Units on order should be positive")]
        public short UnitsOnOrder { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Reorder level should be positive")]
        public short ReorderLevel { get; set; }

        [Required]
        public bool Discontinued { get; set; }

        public int CategoryId { get; set; }

        public int SupplierId { get; set; }
    }
}
