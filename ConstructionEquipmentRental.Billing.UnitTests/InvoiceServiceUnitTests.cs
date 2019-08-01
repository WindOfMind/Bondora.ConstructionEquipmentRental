using System;
using System.Collections.Generic;
using Bondora.ConstructionEquipmentRental.Billing.Services;
using Bondora.ConstructionEquipmentRental.Domain;
using Bondora.ConstructionEquipmentRental.Domain.Interfaces;
using Bondora.ConstructionEquipmentRental.Domain.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Bondora.ConstructionEquipmentRental.Billing.UnitTests
{
    public class InvoiceServiceUnitTests
    {
        [Fact]
        public void GetInvoice_WhenOrderItems_ShouldReturnInvoice()
        {
            // Arrange
            var orderItems = new List<OrderItem>
            {
                new OrderItem {EquipmentName = "TestName", EquipmentType = EquipmentType.Heavy, RentalDays = 1}
            };

            var expectedPoints = 2;

            var loyaltyService = Substitute.For<ILoyaltyService>();
            loyaltyService
                .GetLoyaltyPoints(Arg.Any<IEnumerable<EquipmentType>>())
                .Returns(expectedPoints);

            var rentalFeeService = Substitute.For<IRentalFeeService>();
            rentalFeeService
                .GetRentalFee(Arg.Is(EquipmentType.Heavy), Arg.Is(1))
                .Returns(100.0d);

            string orderId = "test_order_id";

            string expectedInvoice =
                "Bondora Construction Equipment Rental" + Environment.NewLine +
                Environment.NewLine +
                $"Invoice #{orderId}:" + Environment.NewLine +
                Environment.NewLine +
                $"{1}. TestName - {100}{'\u20AC'}" + Environment.NewLine +
                Environment.NewLine +
                $"Total price is {100}{'\u20AC'}." + Environment.NewLine +
                Environment.NewLine +
                "You earned 2 points." + Environment.NewLine +
                Environment.NewLine +
                "Thank you for your order!";

            // Act
            var invoiceService = new InvoiceService(loyaltyService, rentalFeeService);
            string invoice = invoiceService.GetInvoice(orderId, orderItems);

            // Assert
            invoice.Should().Be(expectedInvoice);
        }
    }
}
