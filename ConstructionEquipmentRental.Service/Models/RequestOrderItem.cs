using System.ComponentModel.DataAnnotations;
using Bondora.ConstructionEquipmentRental.Domain;

namespace Bondora.ConstructionEquipmentRental.Service.Models
{
    public class RequestOrderItem : OrderItem
    {
        [MinLength(1)]
        public override string EquipmentName { get; set; }

        [Range(0, 3650)]
        public override int RentalDays { get; set; }
    }
}
