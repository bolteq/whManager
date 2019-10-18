using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using whManagerAPI.Models;
using whManagerLIB.Helpers;
using Microsoft.EntityFrameworkCore;
using whManagerLIB.Models;
using whManagerAPI.Services;

namespace whManagerAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class DeliveryItemTypeController : Controller
    {
        private readonly IDeliveryItemTypeService _deliveryItemTypeService;

        public DeliveryItemTypeController(IDeliveryItemTypeService deliveryItemTypeService)
        {
            _deliveryItemTypeService = deliveryItemTypeService;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetDeliveryItemType([FromRoute] int id)
        {
            var type = await _deliveryItemTypeService.GetDeliveryItemType(id);

            if(type == null) return BadRequest();
            return Ok(type);
        }


        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetDeliveryItemTypes()
        {
            var types = _deliveryItemTypeService.GetDeliveryItemTypes();

            return Ok(types);
        }

        [Authorize(Roles = RoleHelper.Administrator)]
        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult SetDeliveryItemType([FromBody] DeliveryItemType deliveryItemType)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newType = _deliveryItemTypeService.AddDeliveryItemType(deliveryItemType);

            return Ok(newType);

        }

        [Authorize(Roles = RoleHelper.Administrator)]
        [HttpDelete]
        [Route("api/[controller]")]
        public async Task<IActionResult> DeleteDeliveryItemType(int id)
        {
            await _deliveryItemTypeService.DeleteDeliveryItemType(id);
            return Ok();
        }
    }
}