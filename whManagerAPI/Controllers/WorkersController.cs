using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using whManagerAPI.Models;
using whManagerLIB.Models;
using Microsoft.EntityFrameworkCore;

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

        //Metoda: queryWorkers
        //Zwraca: Listę wszystkich 'Workers' z ich 'WorkSchedules' w zależności od wartości bool schedules

        private IQueryable<Worker> queryWorkers(bool schedules)
        {
            if (schedules)
            {
                IQueryable<Worker> query = _context.Workers.Include("WorkSchedules");
                return query;
            }
            else
            {
                IQueryable<Worker> query = _context.Workers;
                return query;
            }
        }

        //Metoda: OnGet
        //Zwraca: IActionResult(IQueryable<Worker>) w zależności od podanych parametrów GET

        [HttpGet]
        public IActionResult OnGet(int? id, string name, string surname, bool schedules)
        {
            IQueryable<Worker> qWorkers = queryWorkers(schedules);

            if(id != null)
            {
                qWorkers = qWorkers
                    .Where(w => w.WorkerId == id);
            }

            // Obsługa 4 sytuacji:
            //  Name    Surname
            //  0       0
            //  0       1
            //  1       0
            //  1       1

            // 1 1
            if(!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surname))
            {
                qWorkers = qWorkers.Where(w => w.Name == name && w.Surname == surname);
            }
            else
            {
                switch (name)
                {
                    //0 0
                    case null when surname == null:
                        break;
                    //0 1
                    case null when !string.IsNullOrEmpty(surname):
                        qWorkers = qWorkers
                            .Where(w => w.Surname == surname);
                        break;
                    //1 0
                    default:
                        qWorkers = qWorkers.Where(w => w.Name == name);
                        break;
                }
            }
            
            return Ok(qWorkers);
        }
    }
}