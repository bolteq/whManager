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

namespace whManagerAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class DeliveryItemTypeController : Controller
    {
        private readonly WHManagerDbContext _context;

        public DeliveryItemTypeController(WHManagerDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetDeliveryItemType([FromRoute] int id)
        {
            var type = await _context
                .DeliveryItemTypes
                .FirstOrDefaultAsync(t => t.Id == id);

            if(type == null)
            {
                return BadRequest();
            }

            return Ok(type);
        }


        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetDeliveryItemTypes()
        {
            var types = _context
                .DeliveryItemTypes;

            return Ok(types);
        }

        [Authorize(Roles = RoleHelper.Administrator)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> SetDeliveryItemType([FromBody] DeliveryItemType deliveryItemType)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            bool bExists = await _context
                .DeliveryItemTypes
                .AnyAsync(dit => dit.Id == deliveryItemType.Id);

            if(bExists)
            {
                _context
                    .DeliveryItemTypes
                    .Update(deliveryItemType);

                await _context
                    .SaveChangesAsync();

                return Ok();
            }
            else
            {
                _context
                    .DeliveryItemTypes
                    .Add(deliveryItemType);

                await _context
                    .SaveChangesAsync();

                return Ok();
            }
        }

        [Authorize(Roles = RoleHelper.Administrator)]
        [HttpDelete]
        [Route("api/[controller]")]
        public async Task<IActionResult> DeleteDeliveryItemType(int id)
        {
            var type = await _context
                .DeliveryItemTypes
                .FirstOrDefaultAsync(dit => dit.Id == id);

            try
            {
                _context
                    .DeliveryItemTypes
                    .Remove(type);

                await _context
                    .SaveChangesAsync();

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }

        }
    }
}