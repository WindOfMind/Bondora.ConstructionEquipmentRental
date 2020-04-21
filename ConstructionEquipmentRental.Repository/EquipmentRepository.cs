using System.Collections.Generic;
using Bondora.ConstructionEquipmentRental.Domain.EquipmentModels;

namespace Bondora.ConstructionEquipmentRental.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private static readonly Dictionary<string, Equipment> Equipment = new Dictionary<string, Equipment>
        {
            {"Caterpillar bulldozer", new HeavyEquipment("Caterpillar bulldozer")},
            {"KamAZ truck", new RegularEquipment("KamAZ truck")},
            {"Komatsu crane", new HeavyEquipment("Komatsu crane")},
            {"Volvo steamroller", new RegularEquipment("Volvo steamroller")},
            {"Bosch jackhammer", new SpecializedEquipment("Bosch jackhammer")}
        };

        public IEnumerable<string> GetEquipmentNames()
        {
            return Equipment.Keys;
        }

        public bool TryGetEquipmentType(string equipmentName, out Equipment equipment)
        {
            return Equipment.TryGetValue(equipmentName, out equipment);
        }

        public bool DoesEquipmentExist(string equipmentName)
        {
            return Equipment.ContainsKey(equipmentName);
        }
    }
}
