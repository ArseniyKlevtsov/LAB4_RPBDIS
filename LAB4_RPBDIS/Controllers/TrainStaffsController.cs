﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LAB4_RPBDIS.Models;

namespace LAB4_RPBDIS.Controllers
{
    public class TrainStaffsController : Controller
    {
        private readonly RailwayTrafficContext _context;

        public TrainStaffsController(RailwayTrafficContext context)
        {
            _context = context;
        }

        // GET: TrainStaffs
        [ResponseCache(CacheProfileName = "Caching")]
        public async Task<IActionResult> Index()
        {
            var railwayTrafficContext = _context.TrainStaffs.Include(t => t.Employee).Include(t => t.Train);
            return View(await railwayTrafficContext.ToListAsync());
        }

        // GET: TrainStaffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrainStaffs == null)
            {
                return NotFound();
            }

            var trainStaff = await _context.TrainStaffs
                .Include(t => t.Employee)
                .Include(t => t.Train)
                .FirstOrDefaultAsync(m => m.TrainStaffId == id);
            if (trainStaff == null)
            {
                return NotFound();
            }

            return View(trainStaff);
        }

        // GET: TrainStaffs/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainId");
            return View();
        }

        // POST: TrainStaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainStaffId,DayOfWeek,TrainId,EmployeeId")] TrainStaff trainStaff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", trainStaff.EmployeeId);
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainId", trainStaff.TrainId);
            return View(trainStaff);
        }

        // GET: TrainStaffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrainStaffs == null)
            {
                return NotFound();
            }

            var trainStaff = await _context.TrainStaffs.FindAsync(id);
            if (trainStaff == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", trainStaff.EmployeeId);
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainId", trainStaff.TrainId);
            return View(trainStaff);
        }

        // POST: TrainStaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainStaffId,DayOfWeek,TrainId,EmployeeId")] TrainStaff trainStaff)
        {
            if (id != trainStaff.TrainStaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainStaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainStaffExists(trainStaff.TrainStaffId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", trainStaff.EmployeeId);
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainId", trainStaff.TrainId);
            return View(trainStaff);
        }

        // GET: TrainStaffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrainStaffs == null)
            {
                return NotFound();
            }

            var trainStaff = await _context.TrainStaffs
                .Include(t => t.Employee)
                .Include(t => t.Train)
                .FirstOrDefaultAsync(m => m.TrainStaffId == id);
            if (trainStaff == null)
            {
                return NotFound();
            }

            return View(trainStaff);
        }

        // POST: TrainStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrainStaffs == null)
            {
                return Problem("Entity set 'RailwayTrafficContext.TrainStaffs'  is null.");
            }
            var trainStaff = await _context.TrainStaffs.FindAsync(id);
            if (trainStaff != null)
            {
                _context.TrainStaffs.Remove(trainStaff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainStaffExists(int id)
        {
          return (_context.TrainStaffs?.Any(e => e.TrainStaffId == id)).GetValueOrDefault();
        }
    }
}
