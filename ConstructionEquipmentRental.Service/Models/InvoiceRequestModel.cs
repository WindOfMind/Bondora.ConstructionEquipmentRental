using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bondora.ConstructionEquipmentRental.Service.Models
{
    public class InvoiceRequestModel
    {
        [Required]
        [MinLength(1)]
        public IEnumerable<RequestOrderItem> OrderItems { get; set; }
    }
}
