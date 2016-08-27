using ECommerce.Data;
using ECommerce.Domain.Entities;
using ECommerce.Repository.Abstract;
using System;
using System.Collections.Generic;

namespace ECommerce.Repository.Concrete
{
    public class EFFeatureRequestRepository : IFeatureRequestRepository
    {
        private readonly EFDbContext context = new EFDbContext();

        public IEnumerable<FeatureRequest> FeatureRequests
        {
            get { return context.FeatureRequests; }
        }

        public void Save(FeatureRequest featureRequest)
        {
            if (featureRequest.Id == 0)
            {
                context.FeatureRequests.Add(featureRequest);
            }
            else
            {
                var dbEntry = context.FeatureRequests.Find(featureRequest.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = featureRequest.Name;
                    dbEntry.Description = featureRequest.Description;
                    dbEntry.ModifiedDate = DateTime.Now;
                    dbEntry.Status = featureRequest.Status;
                    dbEntry.RequestedBy = featureRequest.RequestedBy;
                }
            }

            context.SaveChanges();
        }

        public FeatureRequest DeleteFeatureRequest(int featureRequestId)
        {
            var dbEntry = context.FeatureRequests.Find(featureRequestId);
            if (dbEntry != null)
            {
                context.FeatureRequests.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
