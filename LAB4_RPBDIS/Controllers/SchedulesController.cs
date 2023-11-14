using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB4_RPBDIS.Models;

namespace LAB4_RPBDIS.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly RailwayTrafficContext _context;

        public SchedulesController(RailwayTrafficContext context)
        {
            _context = context;
        }

        // GET: Schedules
        [ResponseCache(CacheProfileName = "Caching")]
        public async Task<IActionResult> Index()
        {
            var railwayTrafficContext = _context.Schedules.Include(s => s.Stop).Include(s => s.Train);
            return View(await railwayTrafficContext.ToListAsync());
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Stop)
                .Include(s => s.Train)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["StopId"] = new SelectList(_context.Stops, "StopId", "StopId");
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainId");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,DayOfWeek,TrainId,StopId,ArrivalTime,DepartureTime")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StopId"] = new SelectList(_context.Stops, "StopId", "StopId", schedule.StopId);
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainId", schedule.TrainId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["StopId"] = new SelectList(_context.Stops, "StopId", "StopId", schedule.StopId);
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainId", schedule.TrainId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,DayOfWeek,TrainId,StopId,ArrivalTime,DepartureTime")] Schedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.ScheduleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StopId"] = new SelectList(_context.Stops, "StopId", "StopId", schedule.StopId);
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainId", schedule.TrainId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Schedules == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Stop)
                .Include(s => s.Train)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Schedules == null)
            {
                return Problem("Entity set 'RailwayTrafficContext.Schedules'  is null.");
            }
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
          return (_context.Schedules?.Any(e => e.ScheduleId == id)).GetValueOrDefault();
        }
    }
}
