using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConductorAppAdmin.Models;

namespace ConductorAppAdmin.Controllers
{
    public class CommunicationController : Controller
    {
        private readonly ConductorAppAdminContext _context;

        public CommunicationController(ConductorAppAdminContext context)
        {
            _context = context;
        }

        // GET: CommunicationModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Communication.ToListAsync());
        }

        // GET: CommunicationModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communicationModel = await _context.Communication
                .SingleOrDefaultAsync(m => m.Id == id);
            if (communicationModel == null)
            {
                return NotFound();
            }

            return View(communicationModel);
        }

        // GET: CommunicationModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CommunicationModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] Communication communicationModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(communicationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(communicationModel);
        }

        // GET: CommunicationModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communicationModel = await _context.Communication.SingleOrDefaultAsync(m => m.Id == id);
            if (communicationModel == null)
            {
                return NotFound();
            }
            return View(communicationModel);
        }

        // POST: CommunicationModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] Communication communicationModel)
        {
            if (id != communicationModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(communicationModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommunicationModelExists(communicationModel.Id))
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
            return View(communicationModel);
        }

        // GET: CommunicationModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communicationModel = await _context.Communication
                .SingleOrDefaultAsync(m => m.Id == id);
            if (communicationModel == null)
            {
                return NotFound();
            }

            return View(communicationModel);
        }

        // POST: CommunicationModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var communicationModel = await _context.Communication.SingleOrDefaultAsync(m => m.Id == id);
            _context.Communication.Remove(communicationModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommunicationModelExists(int id)
        {
            return _context.Communication.Any(e => e.Id == id);
        }
    }
}
