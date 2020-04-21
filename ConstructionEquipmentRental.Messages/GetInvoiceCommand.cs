using System.Collections.Generic;
using Bondora.ConstructionEquipmentRental.Domain;
using NServiceBus;

namespace Bondora.ConstructionEquipmentRental.Messages
{
    public class GetInvoiceCommand : ICommand
    {
        public string ClientId { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
