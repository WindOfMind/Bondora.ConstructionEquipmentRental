using System.Collections.Generic;
using Bondora.ConstructionEquipmentRental.Domain.Models;

namespace Bondora.ConstructionEquipmentRental.Domain.Interfaces
{
    public interface IInvoiceService
    {
        string GetInvoice(string orderId, IEnumerable<OrderItem> orderItems);
    }
}