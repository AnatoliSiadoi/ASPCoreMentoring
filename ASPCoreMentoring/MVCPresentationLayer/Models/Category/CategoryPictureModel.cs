using System.ComponentModel.DataAnnotations;

namespace MVCPresentationLayer.Models.Category
{
    public class CategoryPictureModel
    {

        [Required]
        public int Id { get; set; }

        public byte[] Picture { set; get; }
    }
}
