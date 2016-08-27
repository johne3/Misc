using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ECommerce.Domain.Entities
{
    public class Video
    {
        [HiddenInput(DisplayValue = false)]
        public int VideoId { get; set; }

        [Required(ErrorMessage = "Please enter a video source")]
        public string Source { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
