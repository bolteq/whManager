using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whManagerAPI.Models;

namespace whManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseModelsController : ControllerBase
    {
        private readonly WHManagerDbContext _context;

        public WarehouseModelsController(WHManagerDbContext context)
        {
            _context = context;
        }

        // GET: api/WarehouseModels
        [HttpGet]
        public IEnumerable<WarehouseModel> GetWarehouses()
        {
            return _context.Warehouses;
        }

        // GET: api/WarehouseModels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warehouseModel = await _context.Warehouses.FindAsync(id);

            if (warehouseModel == null)
            {
                return NotFound();
            }

            return Ok(warehouseModel);
        }

        // PUT: api/WarehouseModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouseModel([FromRoute] int id, [FromBody] WarehouseModel warehouseModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != warehouseModel.warehouseId)
            {
                return BadRequest();
            }

            _context.Entry(warehouseModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WarehouseModels
        [HttpPost]
        public async Task<IActionResult> PostWarehouseModel([FromBody] WarehouseModel warehouseModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Warehouses.Add(warehouseModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWarehouseModel", new { id = warehouseModel.warehouseId }, warehouseModel);
        }

        // DELETE: api/WarehouseModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouseModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var warehouseModel = await _context.Warehouses.FindAsync(id);
            if (warehouseModel == null)
            {
                return NotFound();
            }

            _context.Warehouses.Remove(warehouseModel);
            await _context.SaveChangesAsync();

            return Ok(warehouseModel);
        }

        private bool WarehouseModelExists(int id)
        {
            return _context.Warehouses.Any(e => e.warehouseId == id);
        }
    }
}