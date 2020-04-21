using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bondora.ConstructionEquipmentRental.Billing.Services;
using Bondora.ConstructionEquipmentRental.Domain;
using Bondora.ConstructionEquipmentRental.Messages;
using Bondora.ConstructionEquipmentRental.Repository;
using NServiceBus;
using NServiceBus.Logging;

namespace Bondora.ConstructionEquipmentRental.Billing.Handlers
{
    public class GetInvoiceCommandHandler : IHandleMessages<GetInvoiceCommand>
    {
        private static readonly ILog Log = LogManager.GetLogger<GetInvoiceCommandHandler>();
        private readonly BillingService _billingService;

        public GetInvoiceCommandHandler(IEquipmentRepository equipmentRepository)
        {
            if (equipmentRepository == null)
                throw new ArgumentNullException(nameof(equipmentRepository));

            _billingService = new BillingService(equipmentRepository);
        }

        public async Task Handle(GetInvoiceCommand command, IMessageHandlerContext context)
        {
            Log.Info($"Billing has received GetInvoiceMessage, ClientId = {command.ClientId}");

            Invoice invoice = _billingService.GenerateInvoice(command.OrderItems);
            var invoiceMessage = new InvoiceGeneratedMessage
            {
                ClientId = command.ClientId,
                Invoice = invoice.ToString()
            };

            await context.Reply(invoiceMessage);

            Log.Info($"Billing has replied to the calling endpoint with InvoiceGeneratedMessage, ClientId = {command.ClientId}");
        }
    }
}
