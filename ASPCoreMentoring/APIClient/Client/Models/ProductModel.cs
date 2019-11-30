using System.Runtime.Serialization;

namespace APIClient.Client.Models
{
    [DataContract]
    public class ProductModel
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "ProductName")]
        public string ProductName { get; set; }

        [DataMember(Name = "SupplierID")]
        public int SupplierID { get; set; }

        [DataMember(Name = "CategoryID")]
        public int CategoryID { get; set; }

        [DataMember(Name = "QuantityPerUnit")]
        public string QuantityPerUnit { get; set; }

        [DataMember(Name = "UnitPrice")]
        public decimal UnitPrice { get; set; }

        [DataMember(Name = "UnitsInStock")]
        public short UnitsInStock { get; set; }

        [DataMember(Name = "UnitsOnOrder")]
        public short UnitsOnOrder { get; set; }

        [DataMember(Name = "ReorderLevel")]
        public short ReorderLevel { get; set; }

        [DataMember(Name = "Discontinued")]
        public bool Discontinued { get; set; }
    }
}
