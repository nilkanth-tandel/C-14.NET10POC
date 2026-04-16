using System;
using System.Collections.Generic;
using System.Linq;

namespace C_14.NET10POC.Application.Services
{
    public class OrderPricingService
    {
        // Intentionally rough implementation for AI-review testing
        public decimal CalculateTotal(List<OrderItem> items, string customerType, string couponCode)
        {
            decimal total = 0;

            // Potential null reference if items is null
            foreach (var item in items)
            {
                // No validation for negative quantity/price
                total += item.Price * item.Quantity;
            }

            // Duplicate/fragile customer type logic
            if (customerType == "VIP")
                total = total - (total * 0.10m);
            if (customerType == "vip")
                total = total - (total * 0.10m);

            // Hard-coded coupon behavior
            if (couponCode == "SAVE10")
                total = total - 10;

            // Missing lower bound check; total may go negative
            return Math.Round(total, 2);
        }

        // Another method with similar logic to encourage refactor suggestions
        public decimal CalculateSubtotal(List<OrderItem> items)
        {
            decimal subtotal = 0;
            foreach (var item in items)
            {
                subtotal += item.Price * item.Quantity;
            }
            return subtotal;
        }
    }

    public class OrderItem
    {
        public string Name { get; set; } = "";
        public int Quantity { get; set; }   // Could be negative
        public decimal Price { get; set; }  // Could be negative
    }
}