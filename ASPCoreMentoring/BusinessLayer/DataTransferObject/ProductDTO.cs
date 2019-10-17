
namespace BusinessLayer.DataTransferObject
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int SupplierID { get; set; }

        public int CategoryID { get; set; }

        public string QuantityPerUnit { get; set; }

        public decimal UnitPrice { get; set; }

        public short UnitsInStock { get; set; }

        public short UnitsOnOrder { get; set; }

        public short ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public SupplierDTO Supplier { get; set; }

        public CategoryDTO Category { get; set; }
    }
}
