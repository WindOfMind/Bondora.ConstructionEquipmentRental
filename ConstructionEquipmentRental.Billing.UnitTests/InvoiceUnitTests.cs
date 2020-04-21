using System;
using System.Collections.Generic;
using Bondora.ConstructionEquipmentRental.Domain;
using Bondora.ConstructionEquipmentRental.Domain.EquipmentModels;
using FluentAssertions;
using Xunit;

namespace Bondora.ConstructionEquipmentRental.Billing.UnitTests
{
    public class InvoiceUnitTests
    {
        [Fact]
        public void ToString_WhenHaveInvoiceItems_ShouldReturnInvoice()
        {
            // Arrange
            var invoiceItems = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    Equipment = new RegularEquipment("TestName"),
                    RentalDays = 1
                },
                new InvoiceItem
                {
                    Equipment = new HeavyEquipment("TestName2"),
                    RentalDays = 1
                }
            };

            var expectedPoints = 3;
            string orderId = "test_order_id";

            string expectedInvoice =
                "Bondora Construction Equipment Rental" + Environment.NewLine +
                Environment.NewLine +
                $"Invoice #{orderId}:" + Environment.NewLine +
                Environment.NewLine +
                $"{1}. TestName - {160}{'\u20AC'}" + Environment.NewLine +
                $"{2}. TestName2 - {160}{'\u20AC'}" + Environment.NewLine +
                Environment.NewLine +
                $"Total price is {320}{'\u20AC'}." + Environment.NewLine +
                Environment.NewLine +
                $"You earned {expectedPoints} points." + Environment.NewLine +
                Environment.NewLine +
                "Thank you for your order!";

            // Act
            var invoice = new Invoice(orderId, invoiceItems);
            string result = invoice.ToString();

            // Assert
            result.Should().Be(expectedInvoice);
        }

        [Fact]
        public void ToString_WhenNoInvoiceItems_ShouldReturnEmpty()
        {
            // Arrange
            string orderId = "test_order_id";

            string expectedInvoice = String.Empty;

            // Act
            var invoice = new Invoice(orderId, new List<InvoiceItem>());
            string result = invoice.ToString();

            // Assert
            result.Should().Be(expectedInvoice);
        }
    }
}
