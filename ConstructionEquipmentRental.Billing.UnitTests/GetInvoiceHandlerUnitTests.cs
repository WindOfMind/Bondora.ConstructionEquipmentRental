using System.Collections.Generic;
using System.Threading.Tasks;
using Bondora.ConstructionEquipmentRental.Billing.Handlers;
using Bondora.ConstructionEquipmentRental.Domain;
using Bondora.ConstructionEquipmentRental.Domain.Interfaces;
using Bondora.ConstructionEquipmentRental.Messages;
using FluentAssertions;
using NServiceBus.Testing;
using NSubstitute;
using Xunit;

namespace Bondora.ConstructionEquipmentRental.Billing.UnitTests
{
    public class GetInvoiceHandlerUnitTests
    {
        [Fact]
        public async Task Handle_WhenGetInvoiceMessage_ShouldReplyWithInvoice()
        {
            // Arrange
            var invoice = "invoice";
            var clientId = "test_client_id";

            var invoiceService = Substitute.For<IInvoiceService>();
            invoiceService
                .GetInvoice(Arg.Any<string>(), Arg.Any<IEnumerable<OrderItem>>())
                .Returns(invoice);

            var getInvoiceMessage = new GetInvoiceMessage()
            {
                ClientId = clientId,
                OrderItems = new List<OrderItem>()
            };

            var handler = new GetInvoiceHandler(invoiceService);
            var context = new TestableMessageHandlerContext();

            var expectedInvoiceGeneratedMessage = new InvoiceGeneratedMessage()
            {
                ClientId = clientId,
                Invoice = invoice
            };

            // Act
            await handler.Handle(getInvoiceMessage, context);

            // Assert
            context.RepliedMessages.Length.Should().Be(1);
            context.RepliedMessages[0].Message.Should().BeEquivalentTo(expectedInvoiceGeneratedMessage);
        }

    }
}
