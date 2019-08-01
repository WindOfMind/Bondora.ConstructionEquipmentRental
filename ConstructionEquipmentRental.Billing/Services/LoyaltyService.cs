using System;
using System.Collections.Generic;
using System.Linq;
using Bondora.ConstructionEquipmentRental.Domain.Interfaces;
using Bondora.ConstructionEquipmentRental.Domain.Models;

namespace Bondora.ConstructionEquipmentRental.Billing.Services
{
    public class LoyaltyService : ILoyaltyService
    {
        private const int IncreasedPoints = 2;
        private const int StandardPoints = 1;

        public int GetLoyaltyPoints(IEnumerable<EquipmentType> equipmentTypes)
        {
            if (equipmentTypes == null) throw new ArgumentNullException(nameof(equipmentTypes));

            return equipmentTypes.Sum(GetPointsByEquipmentType);
        }

        private int GetPointsByEquipmentType(EquipmentType equipmentType)
        {
            switch (equipmentType)
            {
                case EquipmentType.Heavy:
                    return IncreasedPoints;

                case EquipmentType.Regular:
                case EquipmentType.Specialized:
                    return StandardPoints;

                default:
                    throw new ArgumentOutOfRangeException(nameof(equipmentType), equipmentType, null);
            }
        }
    }
}
