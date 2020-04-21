using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bondora.ConstructionEquipmentRental.Domain
{
    public class Invoice
    {
        private readonly string _orderId;
        private readonly IEnumerable<InvoiceItem> _orderItems;

        private const string CompanyInfo = "Bondora Construction Equipment Rental";
        private const char Euro = '\u20AC';

        public Invoice(string orderId, IEnumerable<InvoiceItem> invoiceItems)
        {
            _orderId = orderId ?? throw new ArgumentNullException(nameof(orderId));
            _orderItems = invoiceItems ?? throw new ArgumentNullException(nameof(invoiceItems));
        }

        public override string ToString()
        {
            InvoiceItem[] orderItemsArray = _orderItems as InvoiceItem[] ?? _orderItems.ToArray();

            if (orderItemsArray.Length == 0)
            {
                return string.Empty;
            }

            double totalPrice = 0;
            int loyaltyPoints = 0;

            var sb = new StringBuilder();

            sb.AppendLine(CompanyInfo);
            sb.AppendLine();

            sb.AppendLine($"Invoice #{_orderId}:");
            sb.AppendLine();

            for (int itemNumber = 0; itemNumber < orderItemsArray.Length; itemNumber++)
            {
                InvoiceItem orderItem = orderItemsArray[itemNumber];
                string name = orderItem.Equipment.Name;
                int rentalDays = orderItem.RentalDays;
                double price = orderItem.Equipment.GetRentalFee(rentalDays);

                sb.AppendLine($"{itemNumber + 1}. {name} - {price}{Euro}");

                loyaltyPoints += orderItem.Equipment.GetLoyaltyPoints();
                totalPrice += orderItem.Equipment.GetRentalFee(orderItem.RentalDays);
            }

            sb.AppendLine();

            sb.AppendLine($"Total price is {totalPrice}{Euro}.");
            sb.AppendLine();

            sb.AppendLine($"You earned {loyaltyPoints} points.");
            sb.AppendLine();

            sb.Append("Thank you for your order!");

            return sb.ToString();
        }
    }
}
