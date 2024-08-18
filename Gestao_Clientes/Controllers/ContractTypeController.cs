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
    public class ContractTypeController : Controller
    {
        private readonly CA_RS11_P2_2_AlexandraMendes_DBContext _context;

        public ContractTypeController(CA_RS11_P2_2_AlexandraMendes_DBContext context)
        {
            _context = context;
        }

        // GET: ContractType
        public async Task<IActionResult> Index()
        {
              return _context.ContractType != null ? 
                          View(await _context.ContractType.ToListAsync()) :
                          Problem("Entity set 'CA_RS11_P2_2_AlexandraMendes_DBContext.ContractType'  is null.");
        }

        // GET: ContractType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContractType == null)
            {
                return NotFound();
            }

            var contractType = await _context.ContractType
                .FirstOrDefaultAsync(m => m.ContractTypeId == id);
            if (contractType == null)
            {
                return NotFound();
            }

            return View(contractType);
        }

        // GET: ContractType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContractType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractTypeId,Description")] ContractType contractType)
        {
            if (ModelState.IsValid)
            {
                bool exists = _context.ContractType.Any(ct => ct.Description == contractType.Description);
                if (exists)
                {
                    ModelState.AddModelError("Description", "The description already exists. Choose a different description.");
                }
                else
                {
                    _context.Add(contractType);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
                return View(contractType);
        }

        // GET: ContractType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContractType == null)
            {
                return NotFound();
            }

            var contractType = await _context.ContractType.FindAsync(id);
            if (contractType == null)
            {
                return NotFound();
            }
            return View(contractType);
        }

        // POST: ContractType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractTypeId,Description")] ContractType contractType)
        {
            if (id != contractType.ContractTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool exists = _context.ContractType
                        .Any(ct => ct.Description == contractType.Description && ct.ContractTypeId != contractType.ContractTypeId);

                    if (exists)
                    {
                        ModelState.AddModelError("Description", "The description already exists. Choose a different description.");
                        return View(contractType);
                    }

                    _context.Update(contractType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractTypeExists(contractType.ContractTypeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "A concurrency error occurred. Please try again.");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contractType);
        }

        // GET: ContractType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContractType == null)
            {
                return NotFound();
            }

            var contractType = await _context.ContractType
                .FirstOrDefaultAsync(m => m.ContractTypeId == id);
            if (contractType == null)
            {
                return NotFound();
            }

            return View(contractType);
        }

        // POST: ContractType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContractType == null)
            {
                return Problem("The 'CA_RS11_P2_2_AlexandraMendes_DBContext.ContractType' entity set is null.");
            }

            var contractType = await _context.ContractType.FindAsync(id);

            if (contractType == null)
            {
                return NotFound();
            }

            bool hasClients = _context.Client.Any(c => c.ContractTypeId == id);
            if (hasClients)
            {
                ViewBag.Error = true;
                ViewBag.Message = "This contract type cannot be deleted because it is associated with one or more clients.";
                return View();
            }

            _context.ContractType.Remove(contractType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractTypeExists(int id)
        {
          return (_context.ContractType?.Any(e => e.ContractTypeId == id)).GetValueOrDefault();
        }
    }
}
