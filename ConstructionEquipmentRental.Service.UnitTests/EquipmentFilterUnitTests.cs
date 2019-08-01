using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bondora.ConstructionEquipmentRental.Domain.Interfaces;
using Bondora.ConstructionEquipmentRental.Service.Filters;
using Bondora.ConstructionEquipmentRental.Service.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using NSubstitute;
using Xunit;

namespace ConstructionEquipmentRental.Service.UnitTests
{
    public class EquipmentFilterUnitTests
    {
        [Fact]
        public async Task OnActionExecutionAsync_WhenOrderItems_ShouldReturnOnlyValidItems()
        {
            // Arrange
            var validOrderItems = new List<RequestOrderItem>
            {
                new RequestOrderItem {EquipmentName = "Valid-Name-1", RentalDays = 1},
                new RequestOrderItem {EquipmentName = "Valid-Name-2", RentalDays = 1}
            };

            var invalidOrderItems = new List<RequestOrderItem>
            {
                new RequestOrderItem {EquipmentName = "Invalid-Name-1", RentalDays = 1},
                new RequestOrderItem {EquipmentName = "Invalid-Name-2", RentalDays = 1}
            };

            IEnumerable<RequestOrderItem> allOrderItems = validOrderItems.Concat(invalidOrderItems);

            IEquipmentRepository equipmentRepository = Substitute.For<IEquipmentRepository>();

            equipmentRepository
                .DoesEquipmentExist(Arg.Is<string>(value => value.Contains("Valid-Name")))
                .Returns(true);

            equipmentRepository
                .DoesEquipmentExist(Arg.Is<string>(value => value.Contains("Invalid-Name")))
                .Returns(false);

            EquipmentFilter equipmentFilter = new EquipmentFilter(equipmentRepository);

            // Act
            var result = await ExecuteFilterForOrderItems(equipmentFilter, allOrderItems);

            // Assert
            result.executingContext.ActionArguments
                .Values
                .Cast<InvoiceRequestModel>()
                .First().OrderItems
                .Should()
                .BeEquivalentTo(validOrderItems);
        }

        private static async Task<(ActionExecutionDelegate actionExecutionDelegate, ActionExecutingContext executingContext)> ExecuteFilterForOrderItems(
            EquipmentFilter filter,
            IEnumerable<RequestOrderItem> orderItems
        )
        {
            IDictionary<string, object> actionArguments = new Dictionary<string, object>
            {
                ["someActionParameter"] = new InvoiceRequestModel { OrderItems = orderItems }
            };

            var actionContext = new ActionContext(
                new DefaultHttpContext(),
                Substitute.For<RouteData>(),

                Substitute.For<ActionDescriptor>());

            var executingContext = new ActionExecutingContext(
                actionContext,
                Substitute.For<IList<IFilterMetadata>>(),
                actionArguments,
                null);

            ActionExecutionDelegate actionExecutionDelegate = Substitute.For<ActionExecutionDelegate>();

            await filter.OnActionExecutionAsync(executingContext, actionExecutionDelegate);

            return (actionExecutionDelegate, executingContext);
        }
    }
}
