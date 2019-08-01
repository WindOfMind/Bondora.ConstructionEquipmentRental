using System.Collections.Generic;
using Bondora.ConstructionEquipmentRental.Billing.Services;
using Bondora.ConstructionEquipmentRental.Domain.Models;
using FluentAssertions;
using Xunit;

namespace Bondora.ConstructionEquipmentRental.Billing.UnitTests
{
    public class LoyaltyServiceUnitTests
    {
        [Fact]
        public void GetLoyaltyPoints_WhenEquipmentTypes_ShouldReturnPoints()
        {
            // Arrange
            var equipmentTypes = new List<EquipmentType>
            {
                EquipmentType.Heavy,
                EquipmentType.Regular,
                EquipmentType.Specialized
            };

            var expectedPoints = 4;

            // Act
            var loyaltyService = new LoyaltyService();
            int points = loyaltyService.GetLoyaltyPoints(equipmentTypes);

            // Assert
            points.Should().Be(expectedPoints);
        }

        [Fact]
        public void GetLoyaltyPoints_WhenNoEquipmentTypes_ShouldReturnZeroPoints()
        {
            // Arrange
            var equipmentTypes = new List<EquipmentType>();
            var expectedPoints = 0;

            // Act
            var loyaltyService = new LoyaltyService();
            int points = loyaltyService.GetLoyaltyPoints(equipmentTypes);

            // Assert
            points.Should().Be(expectedPoints);
        }
    }
}
