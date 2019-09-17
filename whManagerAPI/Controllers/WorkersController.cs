using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using whManagerAPI.Models;
using whManagerLIB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;
using whManagerAPI.Helpers;

namespace whManagerAPI.Controllers
{   [Authorize]
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

        private IQueryable<Worker> QueryWorkers(bool schedules)
        {
            if (schedules)
            {
                IQueryable<Worker> query = _context.Workers.Include(w => w.WorkSchedules);
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
        [Authorize(Roles = RoleHelper.Administrator)]
        [HttpGet]
        public IActionResult OnGet(int? id, string name, string surname, bool schedules)
        {
            IQueryable<Worker> qWorkers = QueryWorkers(schedules);

            if (id != null)
            {
                qWorkers = qWorkers
                    .Where(w => w.WorkerId == id);
                return Ok(qWorkers);
            }

            // Zmienne pomocnicze dla instrukcji switch

            bool bName = string.IsNullOrEmpty(name);
            bool bSurname = string.IsNullOrEmpty(surname);

            switch (bName)
            {
                //When name and surname not specified

                case true when bSurname == true:
                    break;

                //When only surname specified

                case true when bSurname == false:
                    qWorkers = qWorkers
                        .Where(w => w.Surname == surname);
                    break;

                //When only name specified

                case false when bSurname == true:
                    qWorkers = qWorkers
                        .Where(w => w.Name == name);
                    break;

                //When name and surname specified

                case false when bSurname == false:
                    qWorkers = qWorkers
                        .Where(w => w.Name == name && w.Surname == surname);
                    break;
            }

            return Ok(qWorkers);
        }

        [HttpPost]
        public IActionResult OnPost(Worker worker)
        {
            //Jeśli nieprawidłowy status modelu zwróc BadRequest

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //If ID specified and worker exists, try to perform an update

            if (_context.Workers.Any(w => w.WorkerId == worker.WorkerId))
            {
                try
                {
                    _context.Workers.Update(worker);
                    _context.SaveChanges();
                    return Ok(worker);
                }
                catch(DbUpdateException ex)
                {
                    return BadRequest(ex);
                }
            }

            //Create new by default

            try
            {
                _context.Workers.Attach(worker);
                _context.SaveChanges();
                return Ok(worker.WorkerId);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpDelete]
        public IActionResult OnDelete(int id)
        {
            if(_context.Workers.Any(w => w.WorkerId == id))
            {
                try
                {
                    var worker = _context.Workers.Find(id);

                    _context.Workers.Remove(worker);
                    _context.SaveChanges();

                    return Ok($"Worker {id} deleted");
                }
                catch(Exception ex)
                {
                    return BadRequest(ex);
                }
            }

            return BadRequest($"Worker with {id} doesn't exist");
        }
    }
}