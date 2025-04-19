using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public record OrderResult
    {
        public Guid Id { get; init; }
        public string UserEmail { get; init; }

        public AddressDTO ShippingAddress { get; init; }

        public ICollection<OrderItemDto> orderItems { get; init; } = new List<OrderItemDto>();


        public string PaymentStatus { get; init; }

        public string DeliveryMethod { get; init; }

        public decimal SubTotal { get; init; }


        public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.Now;

        public string? PaymentIntentId { get; init; } = string.Empty;

    }
}
