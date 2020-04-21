using System;

namespace Bondora.ConstructionEquipmentRental.Domain.EquipmentModels
{
    public class HeavyEquipment : Equipment
    {
        public HeavyEquipment(string name) : base(name)
        {
        }

        public override EquipmentType EquipmentType => EquipmentType.Heavy;

        public override double GetRentalFee(int rentalDays)
        {
            if (rentalDays <= 0)
                throw new ArgumentOutOfRangeException(nameof(rentalDays));

            return RentalFee.OneTimeRental + RentalFee.PremiumDaily * rentalDays;
        }

        public override int GetLoyaltyPoints()
        {
            return LoyaltyPoint.IncreasedPoints;
        }
    }
}