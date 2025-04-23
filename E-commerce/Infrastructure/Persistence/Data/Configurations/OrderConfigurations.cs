using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, p => p.WithOwner());
            builder.HasMany(o => o.orderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.PaymentStatus).HasConversion
                (paymentStatus => paymentStatus.ToString(),
                s => Enum.Parse<OrderPaymentStatus>(s));

            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);

            builder.Property(d => d.SubTotal).HasColumnType("decimal(18,3)");




        }
    }
}
