using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastracture.Data.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasConversion(
                orderId => orderId.Value,
                dbId => OrderId.Of(dbId)
                );
            builder.HasOne<Customer>().WithMany().HasForeignKey(o => o.CustomerId).IsRequired();
            builder.HasMany(o => o.OrderItems).WithOne().HasForeignKey(oi => oi.OrderId).IsRequired();
            builder.ComplexProperty(o => o.OrderName, buldername =>
            {
                buldername.Property(n => n.Value).HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100).IsRequired();

            });

            builder.ComplexProperty(o => o.ShippingAddress, buldershiping =>
            {
                buldershiping.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                buldershiping.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                buldershiping.Property(a => a.EmailAddress).HasMaxLength(50);
                buldershiping.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();
                buldershiping.Property(a => a.state).HasMaxLength(50);
                buldershiping.Property(a => a.Country).HasMaxLength(50);
                buldershiping.Property(a => a.ZipCode).HasMaxLength(5);

            });
            builder.ComplexProperty(o => o.BillingAddress, bulderbilling =>
            {
                bulderbilling.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                bulderbilling.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                bulderbilling.Property(a => a.EmailAddress).HasMaxLength(50);
                bulderbilling.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();
                bulderbilling.Property(a => a.state).HasMaxLength(50);
                bulderbilling.Property(a => a.Country).HasMaxLength(50);
                bulderbilling.Property(a => a.ZipCode).HasMaxLength(5);

            });
            builder.ComplexProperty(o => o.Payment, bulderpayment =>
            {
                bulderpayment.Property(p => p.CardName).HasMaxLength(50);
                bulderpayment.Property(p => p.CardNumber).HasMaxLength(24).IsRequired();
                bulderpayment.Property(p => p.Expiration).HasMaxLength(10);
                bulderpayment.Property(p => p.Cvv).HasMaxLength(3);
                bulderpayment.Property(p => p.PaymentMethod);
            });
            builder.Property(o=>o.Statues).HasDefaultValue(OrderStatues.Draft)
                .HasConversion(s=>s.ToString(),
                dbState=>(OrderStatues)Enum.Parse(typeof(OrderStatues),dbState)
                );
            builder.Property(o => o.TotalPrice);
        }
    }
}
