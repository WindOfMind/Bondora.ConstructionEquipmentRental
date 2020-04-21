using System;

namespace Bondora.ConstructionEquipmentRental.Domain.EquipmentModels
{
    public class SpecializedEquipment : Equipment
    {
        private const int SpecializedPremiumFeeDays = 3;

        public SpecializedEquipment(string name) : base(name)
        {
        }

        public override EquipmentType EquipmentType => EquipmentType.Specialized;

        public override double GetRentalFee(int rentalDays)
        {
            if (rentalDays <= 0)
                throw new ArgumentOutOfRangeException(nameof(rentalDays));

            int regularRentDays = Math.Max(rentalDays - SpecializedPremiumFeeDays, 0);
            int premiumRentDays = Math.Min(rentalDays, SpecializedPremiumFeeDays);

            return RentalFee.PremiumDaily * premiumRentDays + RentalFee.RegularDaily * regularRentDays;
        }

        public override int GetLoyaltyPoints() => LoyaltyPoint.StandardPoints;
    }
}