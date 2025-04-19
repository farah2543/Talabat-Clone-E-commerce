using ShippingAddress = Domain.Entities.OrderEntities.Address;

namespace Domain.Entities.OrderEntities
{
    public class Order : BaseEntity<Guid>
    {
        public string UserEmail { get; set; }

        public ShippingAddress Address { get; set; }

        public ICollection<OrderItem> orderItems { get; set; } = new List<OrderItem>();


        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;

        public DeliveryMethod DeliveryMethod { get; set; }

        public int? DeliveryMethodId { get; set; }


        public decimal SubTotal { get; set; }


        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public string? PaymentIntentId { get; set; } = string.Empty;

    }
}
