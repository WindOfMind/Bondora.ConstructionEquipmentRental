using System.Threading.Tasks;
using Bondora.ConstructionEquipmentRental.Messages;
using Bondora.ConstructionEquipmentRental.Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using NServiceBus;
using NServiceBus.Logging;

namespace Bondora.ConstructionEquipmentRental.Service.Handlers
{
    public class InvoiceGeneratedHandler : IHandleMessages<InvoiceGeneratedMessage>
    {
        private static readonly ILog Log = LogManager.GetLogger<InvoiceGeneratedHandler>();
        private readonly IHubContext<InvoicesHub> _invoicesHubContext;

        public InvoiceGeneratedHandler(IHubContext<InvoicesHub> invoicesHubContext)
        {
            _invoicesHubContext = invoicesHubContext;
        }

        public async Task Handle(InvoiceGeneratedMessage message, IMessageHandlerContext context)
        {
            Log.Info($"Service has received InvoiceGenerated, ClientId = {message.ClientId}");

            // For demonstration purposes we can assume that we have on;y one user so we can simply use sending all.
            // But in real cases it's better to get the user id from token or provide connection id to the controller (from the client).
            //  _invoicesHubContext.Clients.Client(message.ClientId).SendAsync("invoiceGenerated", content);
            await _invoicesHubContext.Clients.All.SendAsync("InvoiceGenerated", message.Invoice);

            Log.Info($"Invoice has been sent to ClientId = {message.ClientId}");
        }
    }
}
