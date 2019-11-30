using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace APIClient.Client.Models
{
    [DataContract]
    public class CategoryModel
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "CategoryName")]
        public string CategoryName { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "PictureLink")]
        public string PictureLink { get; set; }
    }
}
