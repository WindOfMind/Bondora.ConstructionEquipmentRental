using System;
using System.Threading.Tasks;
using Bondora.ConstructionEquipmentRental.Domain.Interfaces;
using Bondora.ConstructionEquipmentRental.Messages;
using Bondora.ConstructionEquipmentRental.Service.Filters;
using Bondora.ConstructionEquipmentRental.Service.Mappers;
using Bondora.ConstructionEquipmentRental.Service.Models;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;

namespace Bondora.ConstructionEquipmentRental.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IEndpointInstance _endpointInstance;
        private readonly IEquipmentRepository _equipmentRepository;

        public InvoicesController(IEndpointInstance endpointInstance, IEquipmentRepository equipmentRepository)
        {
            _endpointInstance = endpointInstance ?? throw new ArgumentNullException(nameof(endpointInstance));
            _equipmentRepository = equipmentRepository ?? throw new ArgumentNullException(nameof(equipmentRepository));
        }

        [HttpPost]
        [ServiceFilter(typeof(EquipmentFilter))]
        public async Task<IActionResult> GetInvoiceAsync([FromBody]InvoiceRequestModel invoiceRequestModel)
        {
            var getInvoiceCommand = new GetInvoiceMessage
            {
                // ClientId or userId should be retrieved from JWT token once authentication is implemented.
                // But it's out of scope. So here is just stub.
                ClientId = "test_client_Id",
                OrderItems = OrderItemMapper.Map(invoiceRequestModel.OrderItems, _equipmentRepository)
            };

            await _endpointInstance.Send(getInvoiceCommand).ConfigureAwait(false);

            return Accepted();
        }
    }
}
