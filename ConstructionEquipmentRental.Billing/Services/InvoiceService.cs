using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bondora.ConstructionEquipmentRental.Domain;
using Bondora.ConstructionEquipmentRental.Domain.Interfaces;
using Bondora.ConstructionEquipmentRental.Domain.Models;

namespace Bondora.ConstructionEquipmentRental.Billing.Services
{
    public class InvoiceService : IInvoiceService
    {
        private const string CompanyInfo = "Bondora Construction Equipment Rental";
        private const char Euro = '\u20AC';

        private readonly ILoyaltyService _loyaltyService;
        private readonly IRentalFeeService _rentalFeeService;

        public InvoiceService(ILoyaltyService loyaltyService, IRentalFeeService rentalFeeService)
        {
            _loyaltyService = loyaltyService;
            _rentalFeeService = rentalFeeService;
        }

        public string GetInvoice(string orderId, IEnumerable<OrderItem> orderItems)
        {
            OrderItem[] orderItemsArray = orderItems as OrderItem[] ?? orderItems?.ToArray();

            if (orderItemsArray == null)
            {
                throw new ArgumentNullException(nameof(orderItems));
            }

            double totalPrice = 0;
            int loyaltyPoints =
                _loyaltyService.GetLoyaltyPoints(orderItemsArray.Select(orderItem => orderItem.EquipmentType));

            var sb = new StringBuilder();

            sb.AppendLine(CompanyInfo);
            sb.AppendLine();

            sb.AppendLine($"Invoice #{orderId}:");
            sb.AppendLine();

            int lineNumber = 0;
            foreach (OrderItem orderItem in orderItemsArray)
            {
                lineNumber++;

                double rentalPrice = _rentalFeeService.GetRentalFee(orderItem.EquipmentType, orderItem.RentalDays);
                totalPrice += rentalPrice;

                sb.AppendLine(GetLineForOrderItem(lineNumber, orderItem.EquipmentName, rentalPrice));
            }

            sb.AppendLine();

            sb.AppendLine($"Total price is {totalPrice}{Euro}.");
            sb.AppendLine();

            sb.AppendLine($"You earned {loyaltyPoints} points.");
            sb.AppendLine();

            sb.Append("Thank you for your order!");

            return sb.ToString();
        }

        private string GetLineForOrderItem(int lineNumber, string name, double rentalPrice)
        {
            return $"{lineNumber}. {name} - {rentalPrice}{Euro}";
        }
    }
}
