﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPServiceScolarite.Models;

namespace TPServiceScolarite.Controllers
{
    [Authorize]
    public class ModulesController : Controller
    {
        private readonly ScolariteDbEntities _context;
        private IWebHostEnvironment _webHostEnvironment;

        public ModulesController(ScolariteDbEntities context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Modules
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return _context.Modules != null ?
                        View(await _context.Modules.ToListAsync()) :
                        Problem("Entity set 'ScolariteDbEntities.Modules'  is null.");
        }

        // GET: Modules/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // GET: Modules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Resume,Infos,Logo")] Module @module, IFormFile? Logo)
        {
            if (ModelState.IsValid)
            {
                if (Logo != null)
                {
                    string rootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(Logo.FileName) + "_" +
                               Guid.NewGuid() +
                               Path.GetExtension(Logo.FileName);
                    string path = Path.Combine(rootPath + "/photoLogoModule/", fileName);

                    var fileStream = new FileStream(path, FileMode.Create);
                    await Logo.CopyToAsync(fileStream);
                    fileStream.Close();
                    module.Logo = fileName;
                }

                _context.Add(@module);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(@module);
        }

        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }
            return View(@module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Resume,Infos,Logo")] Module @module, IFormFile? Logo)
        {
            if (id != @module.Id)
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
                        string path = Path.Combine(rootPath + "/photoLogoModule/", fileName);

                        var fileStream = new FileStream(path, FileMode.Create);
                        await Logo.CopyToAsync(fileStream);
                        fileStream.Close();
                        @module.Logo = fileName;
                    }
                    _context.Update(@module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(@module.Id))
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
            return View(@module);
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Modules == null)
            {
                return Problem("Entity set 'ScolariteDbEntities.Modules'  is null.");
            }
            var @module = await _context.Modules.FindAsync(id);
            if (@module != null)
            {
                _context.Modules.Remove(@module);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(int id)
        {
            return (_context.Modules?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
