using System;

namespace Bondora.ConstructionEquipmentRental.Domain.EquipmentModels
{
    public class RegularEquipment : Equipment
    {
        private const int RegularPremiumFeeDays = 2;

        public RegularEquipment(string name) : base(name)
        {
        }

        public override EquipmentType EquipmentType => EquipmentType.Regular;

        public override double GetRentalFee(int rentalDays)
        {
            if (rentalDays <= 0)
                throw new ArgumentOutOfRangeException(nameof(rentalDays));

            int regularRentDays = Math.Max(rentalDays - RegularPremiumFeeDays, 0);
            int premiumRentDays = Math.Min(rentalDays, RegularPremiumFeeDays);

            return RentalFee.OneTimeRental
                   + RentalFee.PremiumDaily * premiumRentDays
                   + RentalFee.RegularDaily * regularRentDays;
        }

        public override int GetLoyaltyPoints() => LoyaltyPoint.StandardPoints;
    }
}