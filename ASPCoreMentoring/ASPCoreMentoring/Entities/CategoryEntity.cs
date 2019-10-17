using DataAccessLayer.Entities.Interfaces;

namespace DataAccessLayer.Entities
{
    public class CategoryEntity : IEntity
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
