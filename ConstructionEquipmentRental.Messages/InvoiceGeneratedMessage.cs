using NServiceBus;

namespace Bondora.ConstructionEquipmentRental.Messages
{
    public class InvoiceGeneratedMessage : IMessage
    {
        public string ClientId { get; set; }

        public string Invoice { get; set; }
    }
}
