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
    public class TrainTypesController : Controller
    {
        private readonly RailwayTrafficContext _context;

        public TrainTypesController(RailwayTrafficContext context)
        {
            _context = context;
        }

        // GET: TrainTypes
        [ResponseCache(CacheProfileName = "Caching")]
        public async Task<IActionResult> Index()
        {
              return _context.TrainTypes != null ? 
                          View(await _context.TrainTypes.ToListAsync()) :
                          Problem("Entity set 'RailwayTrafficContext.TrainTypes'  is null.");
        }

        // GET: TrainTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrainTypes == null)
            {
                return NotFound();
            }

            var trainType = await _context.TrainTypes
                .FirstOrDefaultAsync(m => m.TrainTypeId == id);
            if (trainType == null)
            {
                return NotFound();
            }

            return View(trainType);
        }

        // GET: TrainTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainTypeId,TypeName")] TrainType trainType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainType);
        }

        // GET: TrainTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrainTypes == null)
            {
                return NotFound();
            }

            var trainType = await _context.TrainTypes.FindAsync(id);
            if (trainType == null)
            {
                return NotFound();
            }
            return View(trainType);
        }

        // POST: TrainTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainTypeId,TypeName")] TrainType trainType)
        {
            if (id != trainType.TrainTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainTypeExists(trainType.TrainTypeId))
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
            return View(trainType);
        }

        // GET: TrainTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrainTypes == null)
            {
                return NotFound();
            }

            var trainType = await _context.TrainTypes
                .FirstOrDefaultAsync(m => m.TrainTypeId == id);
            if (trainType == null)
            {
                return NotFound();
            }

            return View(trainType);
        }

        // POST: TrainTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrainTypes == null)
            {
                return Problem("Entity set 'RailwayTrafficContext.TrainTypes'  is null.");
            }
            var trainType = await _context.TrainTypes.FindAsync(id);
            if (trainType != null)
            {
                _context.TrainTypes.Remove(trainType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainTypeExists(int id)
        {
          return (_context.TrainTypes?.Any(e => e.TrainTypeId == id)).GetValueOrDefault();
        }
    }
}
