using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gestao_Clientes.DAL;
using Gestao_Clientes.Models;
using static Gestao_Clientes.Models.Enums;

namespace Gestao_Clientes.Controllers
{
    public class PaymentController : Controller
    {
        private readonly CA_RS11_P2_2_AlexandraMendes_DBContext _context;

        public PaymentController(CA_RS11_P2_2_AlexandraMendes_DBContext context)
        {
            _context = context;
        }

        // GET: Payment
        public async Task<IActionResult> Index()
        {
            var cA_RS11_P2_2_AlexandraMendes_DBContext = _context.Payment.Include(p => p.Client);
            return View(await cA_RS11_P2_2_AlexandraMendes_DBContext.ToListAsync());
        }

        // GET: Payment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Payment == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Client)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payment/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FullName");
            ViewData["PaymentMethod"] = new SelectList(Enum.GetValues(typeof(PaymentMethod)).Cast<PaymentMethod>().Select(pm => new { Value = pm, Text = pm.ToString() }), "Value", "Text");
            return View();
        }


        // POST: Payment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,ClientId,PaymentDate,PaymentMethod,Value")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                // Validate if the client exists
                var clientExists = await _context.Client.AnyAsync(c => c.ClientId == payment.ClientId);
                if (!clientExists)
                {
                    ModelState.AddModelError("ClientId", "Client not found.");
                }

                // Check if Value is within the valid range
                if (payment.Value <= 0 || payment.Value > 9999999.99m)
                {
                    ModelState.AddModelError("Value", "Value must be between 0.01 and 9,999,999.99.");
                }

                if (ModelState.IsValid)
                {
                    _context.Add(payment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FullName", payment.ClientId);
            ViewData["PaymentMethod"] = new SelectList(Enum.GetValues(typeof(PaymentMethod)).Cast<PaymentMethod>().Select(pm => new { Value = pm, Text = pm.ToString() }), "Value", "Text", payment.PaymentMethod);
            return View(payment);

        }

        // GET: Payment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FullName", payment.ClientId);
            ViewData["PaymentMethod"] = new SelectList(Enum.GetValues(typeof(PaymentMethod)).Cast<PaymentMethod>().Select(pm => new { Value = pm, Text = pm.ToString() }), "Value", "Text", payment.PaymentMethod);
            return View(payment);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,ClientId,PaymentDate,PaymentMethod,Value")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Validate if the client exists
                    var clientExists = await _context.Client.AnyAsync(c => c.ClientId == payment.ClientId);
                    if (!clientExists)
                    {
                        ModelState.AddModelError("ClientId", "Client not found.");
                        ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FullName", payment.ClientId);
                        ViewData["PaymentMethod"] = new SelectList(Enum.GetValues(typeof(PaymentMethod)).Cast<PaymentMethod>().Select(pm => new { Value = pm, Text = pm.ToString() }), "Value", "Text", payment.PaymentMethod);
                        return View(payment);
                    }

                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
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

            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FullName", payment.ClientId);
            ViewData["PaymentMethod"] = new SelectList(Enum.GetValues(typeof(PaymentMethod)).Cast<PaymentMethod>().Select(pm => new { Value = pm, Text = pm.ToString() }), "Value", "Text", payment.PaymentMethod);
            return View(payment);
        }

        // GET: Payment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Client)
                .FirstOrDefaultAsync(m => m.PaymentId == id);

            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            if (payment != null)
            {
                _context.Payment.Remove(payment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.PaymentId == id);
        }
    }
}
