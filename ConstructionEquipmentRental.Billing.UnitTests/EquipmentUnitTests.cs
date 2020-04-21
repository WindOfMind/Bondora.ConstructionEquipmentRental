using System.Collections.Generic;
using Bondora.ConstructionEquipmentRental.Domain.EquipmentModels;
using FluentAssertions;
using Xunit;

namespace Bondora.ConstructionEquipmentRental.Billing.UnitTests
{
    public class EquipmentUnitTests
    {
        public static IEnumerable<object[]> RentalFeeTestData = new List<object[]>
        {
            new object[] {new RegularEquipment("Test"), 1, 160},
            new object[] {new RegularEquipment("Test"), 2, 220},
            new object[] {new RegularEquipment("Test"), 3, 260},
            new object[] {new SpecializedEquipment("Test"), 1, 60},
            new object[] {new SpecializedEquipment("Test"), 3, 180},
            new object[] {new SpecializedEquipment("Test"), 5, 260},
            new object[] {new HeavyEquipment("Test"), 1, 160},
            new object[] {new HeavyEquipment("Test"), 3, 280}
        };

        [Theory]
        [MemberData(nameof(RentalFeeTestData))]
        public void GetRentalFee_ForDifferentEquipmentAndRentalDays_ShouldReturnFee(Equipment equipment, int rentalDays, double expectedFee)
        {
            // Act
            double fee = equipment.GetRentalFee(rentalDays);

            // Assert
            fee.Should().Be(expectedFee);
        }

        public static IEnumerable<object[]> LoyaltyPointTestData = new List<object[]>
        {
            new object[] {new RegularEquipment("Test"), 1},
            new object[] {new SpecializedEquipment("Test"), 1},
            new object[] {new HeavyEquipment("Test"), 2}
        };

        [Theory]
        [MemberData(nameof(LoyaltyPointTestData))]
        public void GetLoyaltyPoints_ForDifferentEquipment_ShouldReturnPoints(Equipment equipment, int expectedPoints)
        {
            // Act
            double points = equipment.GetLoyaltyPoints();

            // Assert
            points.Should().Be(expectedPoints);
        }
    }
}
