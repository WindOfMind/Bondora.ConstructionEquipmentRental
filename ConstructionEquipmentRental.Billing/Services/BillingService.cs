using System;
using System.Collections.Generic;
using Bondora.ConstructionEquipmentRental.Domain;
using Bondora.ConstructionEquipmentRental.Domain.EquipmentModels;
using Bondora.ConstructionEquipmentRental.Repository;

namespace Bondora.ConstructionEquipmentRental.Billing.Services
{
    public class BillingService
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public BillingService(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository ?? throw new ArgumentNullException(nameof(equipmentRepository));
        }

        public Invoice GenerateInvoice(IEnumerable<OrderItem> orderItems)
        {
            IEnumerable<InvoiceItem> items = MapToInvoiceItems(orderItems);

            string orderId = Guid.NewGuid().ToString();
            Invoice invoice = new Invoice(orderId, items);

            return invoice;
        }

        private IEnumerable<InvoiceItem> MapToInvoiceItems(IEnumerable<OrderItem> orderItems)
        {
            foreach (OrderItem orderItem in orderItems)
            {
                if (_equipmentRepository.TryGetEquipmentType(orderItem.EquipmentName, out Equipment equipment))
                {
                    yield return new InvoiceItem
                    {
                        Equipment = equipment,
                        RentalDays = orderItem.RentalDays
                    };
                }
            }
        }
    }
}
