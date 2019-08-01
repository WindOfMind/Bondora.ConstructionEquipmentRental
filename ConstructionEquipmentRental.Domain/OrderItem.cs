using Bondora.ConstructionEquipmentRental.Domain.Models;

namespace Bondora.ConstructionEquipmentRental.Domain
{
    public class OrderItem
    {
        public string EquipmentName { get; set; }

        public EquipmentType EquipmentType { get; set; }

        public int RentalDays { get; set; }
    }
}
