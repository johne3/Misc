using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ECommerce.Domain.Entities
{
    public class Picture
    {
        [HiddenInput(DisplayValue = false)]
        public int PictureId { get; set; }

        [Required(ErrorMessage = "Please enter an image path")]
        public string ImagePath { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
