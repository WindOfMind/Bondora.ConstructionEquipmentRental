using System;
using Bondora.ConstructionEquipmentRental.Domain.Interfaces;
using Bondora.ConstructionEquipmentRental.Domain.Models;

namespace Bondora.ConstructionEquipmentRental.Billing.Services
{
    public class RentalFeeService : IRentalFeeService
    {
        private const double OneTimeRentalFee = 100.0;
        private const double PremiumDailyFee = 60.0;
        private const double RegularDailyFee = 40.0;
        private const int SpecializedPremiumFeeDays = 3;
        private const int RegularPremiumFeeDays = 2;

        public double GetRentalFee(EquipmentType equipmentType, int rentalDays)
        {
            if (rentalDays <= 0) throw new ArgumentOutOfRangeException(nameof(rentalDays));

            double fee = 0;

            switch (equipmentType)
            {
                case EquipmentType.Heavy:
                    fee = OneTimeRentalFee + PremiumDailyFee * rentalDays;
                    break;

                case EquipmentType.Regular:
                    fee = OneTimeRentalFee
                          + PremiumDailyFee * (rentalDays >= RegularPremiumFeeDays ? RegularPremiumFeeDays : rentalDays)
                          + RegularDailyFee * (rentalDays > RegularPremiumFeeDays ? rentalDays - RegularPremiumFeeDays : 0);
                    break;

                case EquipmentType.Specialized:
                    fee = PremiumDailyFee * (rentalDays >= SpecializedPremiumFeeDays ? SpecializedPremiumFeeDays : rentalDays)
                          + RegularDailyFee * (rentalDays > SpecializedPremiumFeeDays ? rentalDays - SpecializedPremiumFeeDays : 0);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(equipmentType), equipmentType, null);
            }

            return fee;
        }
    }
}
