namespace Bondora.ConstructionEquipmentRental.Domain
{
    public class OrderItem
    {
        public virtual string EquipmentName { get; set; }

        public virtual int RentalDays { get; set; }
    }
}
