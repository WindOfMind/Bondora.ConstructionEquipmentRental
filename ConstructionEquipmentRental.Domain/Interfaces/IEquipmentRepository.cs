using System.Collections.Generic;
using Bondora.ConstructionEquipmentRental.Domain.Models;

namespace Bondora.ConstructionEquipmentRental.Domain.Interfaces
{
    public interface IEquipmentRepository
    {
        IEnumerable<string> GetEquipmentNames();

        bool TryGetEquipmentType(string equipmentName, out EquipmentType equipmentType);

        bool DoesEquipmentExist(string equipmentName);
    }
}