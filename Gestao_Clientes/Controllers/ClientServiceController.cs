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
    public class ClientServiceController : Controller
    {
        private readonly CA_RS11_P2_2_AlexandraMendes_DBContext _context;

        public ClientServiceController(CA_RS11_P2_2_AlexandraMendes_DBContext context)
        {
            _context = context;
        }

        // GET: ClientService
        public async Task<IActionResult> Index()
        {
            var cA_RS11_P2_2_AlexandraMendes_DBContext = _context.ClientService.Include(c => c.Client).Include(c => c.Service);
            return View(await cA_RS11_P2_2_AlexandraMendes_DBContext.ToListAsync());
        }

        // GET: ClientService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClientService == null)
            {
                return NotFound();
            }

            var clientService = await _context.ClientService
                .Include(c => c.Client)
                .Include(c => c.Service)
                .FirstOrDefaultAsync(m => m.ClientServiceId == id);
            if (clientService == null)
            {
                return NotFound();
            }

            return View(clientService);
        }

        // GET: ClientService/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FullName");
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "Description");
            return View();
        }

        // POST: ClientService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientServiceId,ClientId,ServiceId")] ClientService clientService)
        {
            if (ModelState.IsValid)
            {
                bool clientAlreadyHasService = await _context.ClientService
                    .AnyAsync(cs => cs.ClientId == clientService.ClientId && cs.ServiceId == clientService.ServiceId);

                if (clientAlreadyHasService)
                {
                    ModelState.AddModelError("", "This client is already associated to this service.");
                }
                else
                {
                    _context.Add(clientService);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FullName", clientService.ClientId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "Description", clientService.ServiceId);
            return View(clientService);
        }

        // GET: ClientService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClientService == null)
            {
                return NotFound();
            }

            var clientService = await _context.ClientService.FindAsync(id);
            if (clientService == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FullName", clientService.ClientId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "Description", clientService.ServiceId);
            return View(clientService);
        }

        // POST: ClientService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientServiceId,ClientId,ServiceId")] ClientService clientService)
        {
            if (id != clientService.ClientServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check if ClientId and ServiceId still exist
                    bool clientExists = _context.Client.Any(c => c.ClientId == clientService.ClientId);
                    bool serviceExists = _context.Service.Any(s => s.ServiceId == clientService.ServiceId);

                    if (!clientExists)
                    {
                        ModelState.AddModelError("ClientId", "The specified client does not exist.");
                        return View(clientService);
                    }

                    if (!serviceExists)
                    {
                        ModelState.AddModelError("ServiceId", "The specified service does not exist.");
                        return View(clientService);
                    }

                    // Check if the new combination of ClientId and ServiceId is already associated with another record
                    bool duplicateExists = _context.ClientService.Any(cs =>
                        cs.ClientId == clientService.ClientId &&
                        cs.ServiceId == clientService.ServiceId &&
                        cs.ClientServiceId != clientService.ClientServiceId);

                    if (duplicateExists)
                    {
                        ModelState.AddModelError(string.Empty, "This service is already associated with this client.");
                        return View(clientService);
                    }

                    _context.Update(clientService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientServiceExists(clientService.ClientServiceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "A concurrency error occurred. Please try again.");
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the database. Please try again.");
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FullName", clientService.ClientId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "Description", clientService.ServiceId);
            return View(clientService);
        }

        // GET: ClientService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClientService == null)
            {
                return NotFound();
            }

            var clientService = await _context.ClientService
                .Include(c => c.Client)
                .Include(c => c.Service)
                .FirstOrDefaultAsync(m => m.ClientServiceId == id);
            if (clientService == null)
            {
                return NotFound();
            }

            return View(clientService);
        }

        // POST: ClientService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check if the ClientService set is null (ex. if the context is disposed)
            if (_context.ClientService == null)
            {
                return Problem("Entity set 'CA_RS11_P2_2_AlexandraMendes_DBContext.ClientService' is null.");
            }

            // ClientService entity that corresponds to the given ID
            var clientService = await _context.ClientService.FindAsync(id);

            // If the entity was not found, return a NotFound result
            if (clientService == null)
            {
                return NotFound();
            }

            try
            {
                // Attempt to remove the entity from the context
                _context.ClientService.Remove(clientService);

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // If a database update exception occurs, add an error to ModelState
                ModelState.AddModelError(string.Empty, "An error occurred while attempting to delete this client service. It might be associated with other records.");
                // Return to the view with the error message
                return View(clientService);
            }

            // If deletion is successful, redirect to the index action
            return RedirectToAction(nameof(Index));
        }

        private bool ClientServiceExists(int id)
        {
            return (_context.ClientService?.Any(e => e.ClientServiceId == id)).GetValueOrDefault();
        }
    }
}
