using Bondora.ConstructionEquipmentRental.Domain.Models;

namespace Bondora.ConstructionEquipmentRental.Domain.Interfaces
{
    public interface IRentalFeeService
    {
        double GetRentalFee(EquipmentType equipmentType, int rentalDays);
    }
}