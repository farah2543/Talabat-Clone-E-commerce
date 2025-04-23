using ShippingAddress = Domain.Entities.OrderEntities.Address;

namespace Domain.Entities.OrderEntities
{
    public class Order : BaseEntity<Guid>
    {
       
        public Order()
        {
            
        }

        public Order(
            string userEmail,
            ShippingAddress shippingAddress,
            ICollection<OrderItem> orderItems, 
            DeliveryMethod deliveryMethod ,
            decimal subTotal)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            this.orderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
        }

        public string UserEmail { get; set; }

        public ShippingAddress ShippingAddress { get; set; }

        public ICollection<OrderItem> orderItems { get; set; } = new List<OrderItem>();


        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;

        public DeliveryMethod DeliveryMethod { get; set; }

        public int? DeliveryMethodId { get; set; }


        public decimal SubTotal { get; set; }


        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public string? PaymentIntentId { get; set; } = string.Empty;

    }
}
