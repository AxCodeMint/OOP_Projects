using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gestao_Clientes.DAL;
using Gestao_Clientes.Models;

namespace Gestao_Clientes.Controllers
{
    public class FidelizationTypeController : Controller
    {
        private readonly CA_RS11_P2_2_AlexandraMendes_DBContext _context;

        public FidelizationTypeController(CA_RS11_P2_2_AlexandraMendes_DBContext context)
        {
            _context = context;
        }

        // GET: FidelizationType
        public async Task<IActionResult> Index()
        {
              return _context.FidelizationType != null ? 
                          View(await _context.FidelizationType.ToListAsync()) :
                          Problem("Entity set 'CA_RS11_P2_2_AlexandraMendes_DBContext.FidelizationType'  is null.");
        }

        // GET: FidelizationType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FidelizationType == null)
            {
                return NotFound();
            }

            var fidelizationType = await _context.FidelizationType
                .FirstOrDefaultAsync(m => m.FidelizationTypeId == id);
            if (fidelizationType == null)
            {
                return NotFound();
            }

            return View(fidelizationType);
        }

        // GET: FidelizationType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FidelizationType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FidelizationTypeId,Description,Duration,Discount,ServicesMaximum")] FidelizationType fidelizationType)
        {
            if (string.IsNullOrEmpty(fidelizationType.Description))
            {
                ModelState.AddModelError(string.Empty, "Description is required.");
            }

            if (fidelizationType.Duration <= 0 && (fidelizationType.Discount > 0 || fidelizationType.ServicesMaximum > 0))
            {
                ModelState.AddModelError(string.Empty, "Discount and Services Maximum should only be set if the Duration is greater than 0.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(fidelizationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fidelizationType);
        }

            // GET: FidelizationType/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FidelizationType == null)
            {
                return NotFound();
            }

            var fidelizationType = await _context.FidelizationType.FindAsync(id);
            if (fidelizationType == null)
            {
                return NotFound();
            }
            return View(fidelizationType);
        }

        // POST: FidelizationType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FidelizationTypeId,Description,Duration,Discount,ServicesMaximum")] FidelizationType fidelizationType)
        {
            if (id != fidelizationType.FidelizationTypeId)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(fidelizationType.Description))
            {
                ModelState.AddModelError(string.Empty, "Description is required.");
            }

            if (fidelizationType.Duration <= 0 && (fidelizationType.Discount > 0 || fidelizationType.ServicesMaximum > 0))
            {
                ModelState.AddModelError(string.Empty, "Discount and Services Maximum should only be set if the Duration is greater than 0.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fidelizationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FidelizationTypeExists(fidelizationType.FidelizationTypeId))
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
            return View(fidelizationType);
        }

        // GET: FidelizationType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FidelizationType == null)
            {
                return NotFound();
            }

            var fidelizationType = await _context.FidelizationType
                .FirstOrDefaultAsync(m => m.FidelizationTypeId == id);
            if (fidelizationType == null)
            {
                return NotFound();
            }

            return View(fidelizationType);
        }

        // POST: FidelizationType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FidelizationType == null)
            {
                return Problem("Entity set 'CA_RS11_P2_2_AlexandraMendes_DBContext.FidelizationType' is null.");
            }
            var fidelizationType = await _context.FidelizationType.FindAsync(id);

            if (fidelizationType == null)
            {
                return NotFound();
            }

            bool hasClients = _context.Client.Any(c => c.FidelizationTypeId == id);
            if (hasClients)
            {
                ViewBag.Error = true;
                ViewBag.Message = "This fidelization type cannot be deleted because it is associated with one or more clients.";
                return View();
            }

            _context.FidelizationType.Remove(fidelizationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FidelizationTypeExists(int id)
        {
          return (_context.FidelizationType?.Any(e => e.FidelizationTypeId == id)).GetValueOrDefault();
        }
    }
}
