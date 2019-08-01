using System.Collections.Generic;
using Bondora.ConstructionEquipmentRental.Domain;
using Bondora.ConstructionEquipmentRental.Domain.Interfaces;
using Bondora.ConstructionEquipmentRental.Domain.Models;
using Bondora.ConstructionEquipmentRental.Service.Mappers;
using Bondora.ConstructionEquipmentRental.Service.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Bondora.ConstructionEquipmentRental.Service.UnitTests
{
    public class OrderItemMapperUnitTests
    {
        [Fact]
        public void Map_WhenRequestOrderItems_ShouldReturnOrderItemsWithEquipmentTypes()
        {
            // Arrange
            var requestOrderItems = new List<RequestOrderItem>
            {
                new RequestOrderItem {EquipmentName = "Test_Name_1", RentalDays = 1},
                new RequestOrderItem {EquipmentName = "Test_Name_2", RentalDays = 1},
            };

            IEquipmentRepository equipmentRepository = Substitute.For<IEquipmentRepository>();
            equipmentRepository
                .TryGetEquipmentType("Test_Name_1", out Arg.Any<EquipmentType>())
                .Returns(callInfo =>
                {
                    callInfo[1] = EquipmentType.Regular;
                    return true;
                });

            equipmentRepository
                .TryGetEquipmentType("Test_Name_2", out Arg.Any<EquipmentType>())
                .Returns(callInfo =>
                {
                    callInfo[1] = EquipmentType.Heavy;
                    return true;
                });

            var expectedOrderItems = new List<OrderItem>
            {
                new OrderItem {EquipmentName = "Test_Name_1", EquipmentType = EquipmentType.Regular, RentalDays = 1},
                new OrderItem {EquipmentName = "Test_Name_2", EquipmentType = EquipmentType.Heavy, RentalDays = 1}
            };

            // Act
            var result = OrderItemMapper.Map(requestOrderItems, equipmentRepository);

            // Assert
            result.Should().BeEquivalentTo(expectedOrderItems);
        }

        [Fact]
        public void Map_WhenInvalidRequestOrderItems_ShouldSkip()
        {
            // Arrange
            var requestOrderItems = new List<RequestOrderItem>
            {
                new RequestOrderItem {EquipmentName = "Test_Name_1", RentalDays = 1},
                new RequestOrderItem {EquipmentName = "Test_Name_2", RentalDays = 1},
            };

            IEquipmentRepository equipmentRepository = Substitute.For<IEquipmentRepository>();
            equipmentRepository
                .TryGetEquipmentType("Test_Name_1", out Arg.Any<EquipmentType>())
                .Returns(false);

            equipmentRepository
                .TryGetEquipmentType("Test_Name_2", out Arg.Any<EquipmentType>())
                .Returns(false);

            // Act
            var result = OrderItemMapper.Map(requestOrderItems, equipmentRepository);

            // Assert
            result.Should().BeEmpty();
        }
    }
}
