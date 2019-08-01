using System.Collections.Generic;
using Bondora.ConstructionEquipmentRental.Domain.Interfaces;
using Bondora.ConstructionEquipmentRental.Domain.Models;

namespace Bondora.ConstructionEquipmentRental.Equipment
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private static readonly Dictionary<string, EquipmentType> _equipment = new Dictionary<string, EquipmentType>
        {
            {"Caterpillar bulldozer", EquipmentType.Heavy},
            {"KamAZ truck", EquipmentType.Regular},
            {"Komatsu crane", EquipmentType.Heavy},
            {"Volvo steamroller", EquipmentType.Regular},
            {"Bosch jackhammer", EquipmentType.Specialized}
        };

        public IEnumerable<string> GetEquipmentNames()
        {
            return _equipment.Keys;
        }

        public bool TryGetEquipmentType(string equipmentName, out EquipmentType equipmentType)
        {
            return _equipment.TryGetValue(equipmentName, out equipmentType);
        }

        public bool DoesEquipmentExist(string equipmentName)
        {
            return _equipment.ContainsKey(equipmentName);
        }
    }
}
