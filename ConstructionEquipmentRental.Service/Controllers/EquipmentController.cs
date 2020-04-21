using System.Collections.Generic;
using Bondora.ConstructionEquipmentRental.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Bondora.ConstructionEquipmentRental.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public EquipmentController(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        [HttpGet]
        [ResponseCache(Duration = 30)]
        public IEnumerable<string> Get()
        {
            return _equipmentRepository.GetEquipmentNames();
        }
    }
}
