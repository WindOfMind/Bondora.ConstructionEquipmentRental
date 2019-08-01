using Bondora.ConstructionEquipmentRental.Billing.Services;
using Bondora.ConstructionEquipmentRental.Domain.Models;
using FluentAssertions;
using Xunit;

namespace Bondora.ConstructionEquipmentRental.Billing.UnitTests
{
    public class RentalFeeServiceUnitTests
    {
        [Theory]
        [InlineData(EquipmentType.Regular, 1, 160)]
        [InlineData(EquipmentType.Regular, 2, 220)]
        [InlineData(EquipmentType.Regular, 3, 260)]
        [InlineData(EquipmentType.Specialized, 1, 60)]
        [InlineData(EquipmentType.Specialized, 3, 180)]
        [InlineData(EquipmentType.Specialized, 5, 260)]
        [InlineData(EquipmentType.Heavy, 1, 160)]
        [InlineData(EquipmentType.Heavy, 3, 280)]
        public void GetRentalFee_WhenEquipmentTypeAndRentalDays_ShouldReturnFee(EquipmentType equipmentType, int rentalDays, double expectedFee)
        {
            // Act
            var rentalFeeService = new RentalFeeService();
            double fee = rentalFeeService.GetRentalFee(equipmentType, rentalDays);

            // Assert
            fee.Should().Be(expectedFee);
        }
    }
}
