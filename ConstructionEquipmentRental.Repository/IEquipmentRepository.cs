using System.Collections.Generic;
using Bondora.ConstructionEquipmentRental.Domain.EquipmentModels;

namespace Bondora.ConstructionEquipmentRental.Repository
{
    public interface IEquipmentRepository
    {
        IEnumerable<string> GetEquipmentNames();

        bool TryGetEquipmentType(string equipmentName, out Equipment equipment);

        bool DoesEquipmentExist(string equipmentName);
    }
}