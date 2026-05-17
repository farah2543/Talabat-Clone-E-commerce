using Domain.Contracts;
using Domain.Entities.OrderEntities;
using System.Linq.Expressions;

namespace Services.Specifications
{
    internal class OrderWithPaymentIntentIdSpecifications : Specifications<Order>
    {
        public OrderWithPaymentIntentIdSpecifications(string PaymentIntentId) :
            base(o=>o.PaymentIntentId == PaymentIntentId)
        {
        }
    }
}
