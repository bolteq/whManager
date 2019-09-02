using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using whManagerAPI.Models;
using whManagerLIB.Models;

namespace whManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly WHManagerDbContext _context;

        public WorkersController(WHManagerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkers(int? id, string name)
        {
            List<Worker> workersToReturn = new List<Worker>();
            if(id != null)
            {
                Worker worker = await _context.Workers.FindAsync(id);
                worker.WorkSchedules = (_context.WorkSchedules.Where(workSchedule => workSchedule.WorkerId == worker.WorkerId)).ToList();
                workersToReturn.Add(worker);
            }
            else    
            {
                List<Worker> workers = _context.Workers.ToList();
                foreach(Worker worker in workers)
                {
                    worker.WorkSchedules = (_context.WorkSchedules.Where(workSchedule => workSchedule.WorkerId == worker.WorkerId)).ToList();
                    workersToReturn.Add(worker);
                }
            }
            return Ok(workersToReturn);
        }
    }
}