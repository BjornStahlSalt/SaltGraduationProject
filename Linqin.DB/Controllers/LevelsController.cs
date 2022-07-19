using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Linqin.DB.Models;
using Linqin.DB.Data;

namespace Linqin.DB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LevelsController : Controller
    {
        private readonly LevelContext _context;
        private readonly LevelRepository _storage;

        public LevelsController(LevelContext context)
        {
            _storage = new LevelRepository();
            _context = context;
        }

        // GET: Levels
        // public async Task<IActionResult> Index()
        // {
        //   return _context.Level != null ?
        //               View(await _context.Level.ToListAsync()) :
        //               Problem("Entity set 'LevelContext.Level'  is null.");
        // }

        // GET: Levels/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //   if (id == null || _context.Level == null)
        //   {
        //     return NotFound();
        //   }

        //   var level = await _context.Level
        //       .FirstOrDefaultAsync(m => m.Id == id);
        //   if (level == null)
        //   {
        //     return NotFound();
        //   }

        //   return View(level);
        // }

        // GET: Levels/Create
        // public IActionResult Create()
        // {
        //   return View();
        // }

        // POST: Levels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpGet("{Id}")]
        public async Task<ActionResult<GetResponse>> GetOneLevel(string Id)
        {
            return Ok(_storage.GetData(Id));

        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostRequest request)
        {
            var id = _storage.AddData(request);
            return CreatedAtAction(nameof(GetOneLevel), new { id = id }, request);
        }

        // // GET: Levels/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //   if (id == null || _context.Level == null)
        //   {
        //     return NotFound();
        //   }

        //   var level = await _context.Level.FindAsync(id);
        //   if (level == null)
        //   {
        //     return NotFound();
        //   }
        //   return View(level);
        // }

        // // POST: Levels/Edit/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("Id,Title,LinqMethod,Description")] Level level)
        // {
        //   if (id != level.Id)
        //   {
        //     return NotFound();
        //   }

        //   if (ModelState.IsValid)
        //   {
        //     try
        //     {
        //       _context.Update(level);
        //       await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //       if (!LevelExists(level.Id))
        //       {
        //         return NotFound();
        //       }
        //       else
        //       {
        //         throw;
        //       }
        //     }
        //     return RedirectToAction(nameof(Index));
        //   }
        //   return View(level);
        // }

        // // GET: Levels/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //   if (id == null || _context.Level == null)
        //   {
        //     return NotFound();
        //   }

        //   var level = await _context.Level
        //       .FirstOrDefaultAsync(m => m.Id == id);
        //   if (level == null)
        //   {
        //     return NotFound();
        //   }

        //   return View(level);
        // }

        // // POST: Levels/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //   if (_context.Level == null)
        //   {
        //     return Problem("Entity set 'LevelContext.Level'  is null.");
        //   }
        //   var level = await _context.Level.FindAsync(id);
        //   if (level != null)
        //   {
        //     _context.Level.Remove(level);
        //   }

        //   await _context.SaveChangesAsync();
        //   return RedirectToAction(nameof(Index));
        // }

        private bool LevelExists(string id)
        {
            return (_context.Level?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
