using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MVCPresentationLayer.Models.Category
{
    public class CategoryPictureViewModel
    {
        public int Id { get; set; }

        [Required]
        public IFormFile Picture { set; get; }
    }
}
