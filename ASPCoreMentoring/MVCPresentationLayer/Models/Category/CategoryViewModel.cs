using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Models.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Category name")]
        public string CategoryName { get; set; }

        [Display(Name = "Category description")]
        public string Description { get; set; }
    }
}
