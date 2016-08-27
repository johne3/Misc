using ECommerce.Domain.Entities;

namespace ECommerce.Repository.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
