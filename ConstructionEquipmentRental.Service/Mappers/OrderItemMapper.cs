using System.Collections.Generic;
using Bondora.ConstructionEquipmentRental.Domain;
using Bondora.ConstructionEquipmentRental.Domain.Interfaces;
using Bondora.ConstructionEquipmentRental.Domain.Models;
using Bondora.ConstructionEquipmentRental.Service.Models;

namespace Bondora.ConstructionEquipmentRental.Service.Mappers
{
    public static class OrderItemMapper
    {
        public static IEnumerable<OrderItem> Map(IEnumerable<RequestOrderItem> requestOrderItems,
            IEquipmentRepository equipmentRepository)
        {
            foreach (RequestOrderItem requestOrderItem in requestOrderItems)
            {
                if (equipmentRepository.TryGetEquipmentType(requestOrderItem.EquipmentName, out EquipmentType equipmentType))
                {
                    yield return new OrderItem
                    {
                        EquipmentName = requestOrderItem.EquipmentName,
                        EquipmentType = equipmentType,
                        RentalDays = requestOrderItem.RentalDays
                    };
                }
            }
        }
    }
}
