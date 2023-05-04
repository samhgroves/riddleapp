using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Jokes.Data;
using Jokes.Models;

namespace Jokes.Controllers
{
    public class RiddleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RiddleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Riddle
        public async Task<IActionResult> Index()
        {
              return _context.Riddle != null ? 
                          View(await _context.Riddle.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Riddle'  is null.");
        }

        // GET: Riddle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Riddle == null)
            {
                return NotFound();
            }

            var riddle = await _context.Riddle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (riddle == null)
            {
                return NotFound();
            }

            return View(riddle);
        }

        // GET: Riddle/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Riddle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RiddleQuestion,RiddleAnswer")] Riddle riddle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(riddle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(riddle);
        }

        // GET: Riddle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Riddle == null)
            {
                return NotFound();
            }

            var riddle = await _context.Riddle.FindAsync(id);
            if (riddle == null)
            {
                return NotFound();
            }
            return View(riddle);
        }

        // POST: Riddle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RiddleQuestion,RiddleAnswer")] Riddle riddle)
        {
            if (id != riddle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(riddle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiddleExists(riddle.Id))
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
            return View(riddle);
        }

        // GET: Riddle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Riddle == null)
            {
                return NotFound();
            }

            var riddle = await _context.Riddle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (riddle == null)
            {
                return NotFound();
            }

            return View(riddle);
        }

        // POST: Riddle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Riddle == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Riddle'  is null.");
            }
            var riddle = await _context.Riddle.FindAsync(id);
            if (riddle != null)
            {
                _context.Riddle.Remove(riddle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RiddleExists(int id)
        {
          return (_context.Riddle?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
