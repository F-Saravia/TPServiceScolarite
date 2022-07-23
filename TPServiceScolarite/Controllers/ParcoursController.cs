using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPServiceScolarite.Models;

namespace TPServiceScolarite.Controllers
{
    public class ParcoursController : Controller
    {
        private readonly ScolariteDbEntities _context;
        private IWebHostEnvironment _webHostEnvironment;

        public ParcoursController(ScolariteDbEntities context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Parcours
        public async Task<IActionResult> Index()
        {
            return _context.Parcours != null ?
                        View(await _context.Parcours.ToListAsync()) :
                        Problem("Entity set 'ScolariteDbEntities.Parcours'  is null.");
        }

        // GET: Parcours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Parcours == null)
            {
                return NotFound();
            }

            var parcour = await _context.Parcours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcour == null)
            {
                return NotFound();
            }

            return View(parcour);
        }

        // GET: Parcours/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parcours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Resume,Infos,Logo")] Parcour parcour, IFormFile Logo)
        {
            if (ModelState.IsValid)
            {
                string rootPath = _webHostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(Logo.FileName) + "_" +
                           Guid.NewGuid() +
                           Path.GetExtension(Logo.FileName);
                string path = Path.Combine(rootPath + "/photoLogoParcour/", fileName);

                var fileStream = new FileStream(path, FileMode.Create);
                await Logo.CopyToAsync(fileStream);
                fileStream.Close();
                parcour.Logo = fileName;

                _context.Add(parcour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parcour);
        }

        // GET: Parcours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Parcours == null)
            {
                return NotFound();
            }

            var parcour = await _context.Parcours.FindAsync(id);
            if (parcour == null)
            {
                return NotFound();
            }
            return View(parcour);
        }

        // POST: Parcours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Resume,Infos, Logo")] Parcour parcour, IFormFile? Logo)
        {
            if (id != parcour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Logo != null)
                    {
                        string rootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(Logo.FileName) + "_" +
                                   Guid.NewGuid() +
                                   Path.GetExtension(Logo.FileName);
                        string path = Path.Combine(rootPath + "/photoLogoParcour/", fileName);

                        var fileStream = new FileStream(path, FileMode.Create);
                        await Logo.CopyToAsync(fileStream);
                        fileStream.Close();
                        parcour.Logo = fileName;
                    }

                    _context.Update(parcour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParcourExists(parcour.Id))
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
            return View(parcour);
        }

        // GET: Parcours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Parcours == null)
            {
                return NotFound();
            }

            var parcour = await _context.Parcours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcour == null)
            {
                return NotFound();
            }

            return View(parcour);
        }

        // POST: Parcours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Parcours == null)
            {
                return Problem("Entity set 'ScolariteDbEntities.Parcours'  is null.");
            }
            var parcour = await _context.Parcours.FindAsync(id);
            if (parcour != null)
            {
                _context.Parcours.Remove(parcour);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParcourExists(int id)
        {
            return (_context.Parcours?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
