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
    public class ClientController : Controller
    {
        private readonly CA_RS11_P2_2_AlexandraMendes_DBContext _context;

        public ClientController(CA_RS11_P2_2_AlexandraMendes_DBContext context)
        {
            _context = context;
        }

        // GET: Client
        public async Task<IActionResult> Index()
        {
            var cA_RS11_P2_2_AlexandraMendes_DBContext = _context.Client.Include(c => c.ContractType).Include(c => c.FidelizationType);
            return View(await cA_RS11_P2_2_AlexandraMendes_DBContext.ToListAsync());
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.ContractType)
                .Include(c => c.FidelizationType)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }


        // GET: Client/Create
        public IActionResult Create()
        {
            ViewData["ContractTypeId"] = new SelectList(_context.ContractType, "ContractTypeId", "Description");
            ViewData["FidelizationTypeId"] = new SelectList(_context.FidelizationType, "FidelizationTypeId", "Description");
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,ContractTypeId,FidelizationTypeId,FirstName,LastName,Street,DoorAndFloor,City,PostalCode,FiscalCode,Email,Phone")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractTypeId"] = new SelectList(_context.ContractType, "ContractTypeId", "Description", client.ContractTypeId);
            ViewData["FidelizationTypeId"] = new SelectList(_context.FidelizationType, "FidelizationTypeId", "Description", client.FidelizationTypeId);
            return View(client);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["ContractTypeId"] = new SelectList(_context.ContractType, "ContractTypeId", "Description", client.ContractTypeId);
            ViewData["FidelizationTypeId"] = new SelectList(_context.FidelizationType, "FidelizationTypeId", "Description", client.FidelizationTypeId);
            return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,ContractTypeId,FidelizationTypeId,FirstName,LastName,Street,DoorAndFloor,City,PostalCode,FiscalCode,Email,Phone")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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
            ViewData["ContractTypeId"] = new SelectList(_context.ContractType, "ContractTypeId", "Description", client.ContractTypeId);
            ViewData["FidelizationTypeId"] = new SelectList(_context.FidelizationType, "FidelizationTypeId", "Description", client.FidelizationTypeId);
            return View(client);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.ContractType)
                .Include(c => c.FidelizationType)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }


        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Client == null)
            {
                return Problem("Entity set 'CA_RS11_P2_2_AlexandraMendes_DBContext.Client'  is null.");
            }
            var client = await _context.Client.FindAsync(id);
            if (client != null)
            {
                _context.Client.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return (_context.Client?.Any(e => e.ClientId == id)).GetValueOrDefault();
        }
    }
}
