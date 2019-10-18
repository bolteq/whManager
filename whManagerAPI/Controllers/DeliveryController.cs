using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using whManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using whManagerAPI.Helpers;
using whManagerLIB.Models;
using whManagerLIB.Helpers;
using System;
using System.Security.Claims;
using whManagerAPI.Services;

namespace whManagerAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class DeliveryController : Controller
    {


        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        #region GetDelivery
        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetDelivery([FromRoute] int id)
        {
            var delivery = await _deliveryService.GetDelivery(id);

            if (delivery == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(delivery);
            }
        }
        #endregion

        #region GetDeliveries

        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetDeliveries()
        {
            var deliveries = _deliveryService.GetDeliveries();

            return Ok(deliveries);
        }
        #endregion

        #region SetDelivery
        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> SetDelivery([FromBody] Delivery delivery)
        {
            var newDelivery = await _deliveryService.SetDelivery(delivery);

            if (newDelivery == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(newDelivery);
            }
        }
        #endregion

        #region DeleteDelivery
        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpDelete]
        [Route("api/[controller]")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            var bDeliveryDeleted = await _deliveryService.DeleteDelivery(id);

            if (bDeliveryDeleted)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
    #endregion
}
