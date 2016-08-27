using System;

namespace ECommerce.Domain.Entities
{
    public class FeatureRequest
    {
        public FeatureRequest()
        {
            CreateDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string RequestedBy { get; set; }
        public string Status { get; set; }
    }
}
