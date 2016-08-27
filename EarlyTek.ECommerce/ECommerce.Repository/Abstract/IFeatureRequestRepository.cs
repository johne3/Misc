using ECommerce.Domain.Entities;
using System.Collections.Generic;

namespace ECommerce.Repository.Abstract
{
    public interface IFeatureRequestRepository
    {
        IEnumerable<FeatureRequest> FeatureRequests { get; }
        void Save(FeatureRequest featureRequest);
        FeatureRequest DeleteFeatureRequest(int featureRequestId);
    }
}
