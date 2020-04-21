using System;

namespace Bondora.ConstructionEquipmentRental.Domain.EquipmentModels
{
    public abstract class Equipment
    {
        protected Equipment(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public abstract EquipmentType EquipmentType { get; }

        /// <exception cref="ArgumentOutOfRangeException">Thrown if rentalDays is zero or less.</exception>
        public abstract double GetRentalFee(int rentalDays);

        public abstract int GetLoyaltyPoints();
    }
}
