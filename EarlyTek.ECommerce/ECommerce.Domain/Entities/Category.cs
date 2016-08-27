using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace ECommerce.Domain.Entities
{
    [DataContract]
    public class Category
    {
        [DataMember]
        [HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Please enter a category name")]
        public string Name { get; set; }


        public IEnumerable<Product> Products { get; set; }
    }
}
