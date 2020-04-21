using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bondora.ConstructionEquipmentRental.Repository;
using Bondora.ConstructionEquipmentRental.Service.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using NServiceBus.Logging;

namespace Bondora.ConstructionEquipmentRental.Service.Filters
{
    public class EquipmentFilter : Attribute, IAsyncActionFilter
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private static readonly ILog Log = LogManager.GetLogger<EquipmentFilter>();

        public EquipmentFilter(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository ?? throw new ArgumentNullException(nameof(equipmentRepository));
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            List<InvoiceRequestModel> models = context.ActionArguments.Values.OfType<InvoiceRequestModel>().ToList();

            if (models.Count > 1)
            {
                throw new ArgumentException($"Controller action can't have more than one {nameof(InvoiceRequestModel)} parameter.");
            }

            InvoiceRequestModel model = models.FirstOrDefault();

            if (model == null)
            {
                await next();
                return;
            }

            List<RequestOrderItem> orderItems = model.OrderItems.ToList();

            foreach (RequestOrderItem requestOrderItem in model.OrderItems)
            {
                // For demonstration purposes we just remove invalid equipment items, but
                // it's better to send a response with invalid items.
                if (!_equipmentRepository.DoesEquipmentExist(requestOrderItem.EquipmentName))
                {
                    Log.Warn($"Invalid equipment name {requestOrderItem.EquipmentName}.");
                    orderItems.Remove(requestOrderItem);
                }
            }

            model.OrderItems = orderItems;

            await next();
        }
    }
}
