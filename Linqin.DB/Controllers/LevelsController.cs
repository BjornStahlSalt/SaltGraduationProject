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
  public class LevelsController : ControllerBase
  {
    private readonly LevelRepository _storage;

    public LevelsController()
    {
      _storage = new LevelRepository();
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

    [HttpGet("{id}")]
    public async Task<ActionResult<GetResponse>> GetOneLevel(string id)
    {
      return Ok(_storage.GetData(id));
    }

    // GET: Levels/Edit/5
    [HttpGet]
    public async Task<ActionResult<List<GetResponse>>> GetAllLevels()
    {
      return Ok(_storage.GetAllData());
      // if (id == null || _context.Level == null)
      // {
      //   return NotFound();
      // }

      // var level = await _context.Level.FindAsync(id);
      // if (level == null)
      // {
      //   return NotFound();
      // }
      // return View(level);
    }

    [HttpDelete("{Id}")]
    public IActionResult DeleteLevel(string Id)
    {
      _storage.DeleteData(Id);
      return NoContent();
    }


    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(PostRequest request)
    {
      var id = _storage.AddData(request);
      return CreatedAtAction(nameof(GetOneLevel), new { id = id }, request);
    }




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

    // POST: Levels/Delete/5


  }
}
