using Bondora.ConstructionEquipmentRental.Domain.EquipmentModels;

namespace Bondora.ConstructionEquipmentRental.Domain
{
    public class InvoiceItem
    {
        public Equipment Equipment { get; set; }

        public int RentalDays { get; set; }
    }
}
