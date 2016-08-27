using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class Thumbnail
    {
        [ForeignKey("Product")]
        public int ThumbnailId { get; set; }

        public string ImagePath { get; set; }

        public Product Product { get; set; }
    }
}
