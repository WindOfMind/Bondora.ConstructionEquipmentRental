using System;
using System.Threading.Tasks;
using Bondora.ConstructionEquipmentRental.Domain.Interfaces;
using Bondora.ConstructionEquipmentRental.Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Bondora.ConstructionEquipmentRental.Billing.Handlers
{
    public class GetInvoiceHandler : IHandleMessages<GetInvoiceMessage>
    {
        private static readonly ILog Log = LogManager.GetLogger<GetInvoiceHandler>();

        private readonly IInvoiceService _invoiceService;

        public GetInvoiceHandler(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async Task Handle(GetInvoiceMessage message, IMessageHandlerContext context)
        {
            Log.Info($"Billing has received GetInvoiceMessage, ClientId = {message.ClientId}");

            string invoice = _invoiceService.GetInvoice(Guid.NewGuid().ToString(), message.OrderItems);

            var invoiceMessage = new InvoiceGeneratedMessage
            {
                ClientId = message.ClientId,
                Invoice = invoice
            };

            await context.Reply(invoiceMessage);

            Log.Info($"Billing has replied to the calling endpoint with InvoiceGeneratedMessage, ClientId = {message.ClientId}");
        }
    }
}
