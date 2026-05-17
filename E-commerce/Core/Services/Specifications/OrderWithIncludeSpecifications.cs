using Domain.Contracts;
using Domain.Entities.OrderEntities;

namespace Services.Specifications
{
    public class OrderWithIncludesSpecifications : Specifications<Order>
    {
        // Get single order by ID (with includes)
        public OrderWithIncludesSpecifications(Guid id) : base(o => o.Id == id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.orderItems);
        }

        // Get all orders by user email (with includes and sorting)
        public OrderWithIncludesSpecifications(string email) : base(o => o.UserEmail == email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.orderItems);
            SetOrderBy(o => o.OrderDate);
        }
    }
}
