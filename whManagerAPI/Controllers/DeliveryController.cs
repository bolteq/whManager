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

namespace whManagerAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class DeliveryController : Controller
    {


        private readonly WHManagerDbContext _context;

        public DeliveryController(WHManagerDbContext context)
        {
            _context = context;
        }

        #region GetDelivery
        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetDelivery([FromRoute] int id)
        {
            bool isSpedytor = HttpContext
                .User
                .Claims
                .Any(c => c.Value == RoleHelper.Spedytor);

            bool isKierowca = HttpContext
                .User
                .Claims
                .Any(c => c.Value == RoleHelper.Kierowca);

            var companyId = HttpContext.User.Claims
                    .Where(c => c.Type == MyClaims.CompanyId)
                    .Select(c => int.Parse(c.Value))
                    .FirstOrDefault();

            var username = HttpContext.User.Claims
                .Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value)
                .FirstOrDefault();

            var delivery = await _context.Deliveries
                                            .Include(d => d.Car)
                                            .Include(d => d.Trailer)
                                            .Include(d => d.Company)
                                            .Include(d => d.User)
                                            .Include(d => d.DeliveryItems)
                                            .FirstOrDefaultAsync(d => d.Id == id);
                                            
                

            if (isSpedytor && delivery.CompanyId != companyId)
            {
                return Unauthorized();
            }

            if (isKierowca && delivery.User.EmailAddress != username)
            {
                return Unauthorized();
            }

            try
            {
                return Ok(delivery);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
        #endregion

        #region GetDeliveries

        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetDeliveries()
        {
            bool isSpedytor = HttpContext.User.Claims.Any(c => c.Value == RoleHelper.Spedytor);
            var companyId = HttpContext.User.Claims
                    .Where(c => c.Type == MyClaims.CompanyId)
                    .Select(c => int.Parse(c.Value))
                    .FirstOrDefault();

            switch (isSpedytor)
            {
                case true:
                    var companyDeliveries = _context
                        .Deliveries
                        .Where(d => d.CompanyId == companyId)
                            .Include(d => d.Car)
                            .Include(d => d.Trailer)
                            .Include(d => d.Company)
                            .Include(d => d.User)
                            .Include(d => d.DeliveryItems);
                  

                    return Ok(companyDeliveries);
                case false:
                    var allDeliveries = _context
                        .Deliveries
                        .Include(d => d.Car)
                        .Include(d => d.Trailer)
                        .Include(d => d.Company)
                        .Include(d => d.User)
                        .Include(d => d.DeliveryItems);

                    return Ok(allDeliveries);
            }

            return BadRequest();
        }
        #endregion

        #region SetDelivery
        [Authorize(Roles = RoleHelper.SpedytorAdministrator)]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> SetDelivery([FromBody] Delivery delivery)
        {
            var companyId = HttpContext.User.Claims
                .Where(c => c.Type == MyClaims.CompanyId)
                .Select(c => int.Parse(c.Value))
                .FirstOrDefault();

            bool bExists = await _context
                .Deliveries
                .AnyAsync(d => d.Id == delivery.Id);

            try
            {
                switch (bExists)
                {
                    case true:
                        if(delivery.CompanyId != companyId)
                        {
                            return Unauthorized();
                        }
                        _context
                            .Deliveries
                            .Update(delivery);
                        await _context.SaveChangesAsync();

                        return Ok();
                    case false:
                        delivery.CompanyId = companyId;

                        _context.Deliveries.Add(delivery);
                        await _context.SaveChangesAsync();

                        return Ok();
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.ToString());
            }
            return BadRequest();
        }
        #endregion
    }

}
