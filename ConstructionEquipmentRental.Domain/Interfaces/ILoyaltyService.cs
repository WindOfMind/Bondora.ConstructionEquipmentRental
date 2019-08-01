using System.Collections.Generic;
using Bondora.ConstructionEquipmentRental.Domain.Models;

namespace Bondora.ConstructionEquipmentRental.Domain.Interfaces
{
    public interface ILoyaltyService
    {
        int GetLoyaltyPoints(IEnumerable<EquipmentType> equipmentTypes);
    }
}