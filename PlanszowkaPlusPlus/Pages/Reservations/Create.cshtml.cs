using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlanszowkaPlusPlus.Data;
using PlanszowkaPlusPlus.Models;
using Microsoft.EntityFrameworkCore;


namespace PlanszowkaPlusPlus.Pages.Reservations
{
    [Authorize(AuthenticationSchemes = "MyCookieAuth", Roles = "Employee,Admin")]
    public class CreateModel : PageModel
    {
        private readonly PlanszowkaPlusPlus.Data.AppDbContext _context;

        public CreateModel(PlanszowkaPlusPlus.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
{
            var tables = _context.GameTables.ToList();
            var members = _context.Members.ToList();

            if (tables.Count == 0 || members.Count == 0)
            {
                //optionally handle the case where there are no tables or members in the database
                ModelState.AddModelError(string.Empty, "No tables or members available to create a reservation.");
            }

            ViewData["TableId"] = new SelectList(tables, "Id", "Id");
            ViewData["MemberId"] = new SelectList(members, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public ReservationDTO ReservationInfo { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            GameTable? table = await _context.GameTables.FindAsync(ReservationInfo.TableId);
            Member? member = await _context.Members.FindAsync(ReservationInfo.MemberId);

            if (null == table)
            {
                ModelState.AddModelError("ReservationInfo.TableId", "The selected table no longer exists.");
                return Page();
            }

            if (null == member)
            {
                ModelState.AddModelError("ReservationInfo.MemberId", "The selected member no longer exists.");
                return Page();
            }

            _context.Reservations.Add(new Reservation
            {
                ReservationDate = ReservationInfo.ReservationDate,
                TimeStart = ReservationInfo.TimeStart,
                TimeEnd = ReservationInfo.TimeEnd,
                IsArchived = false,
                TableId = table.Id,
                MemberId = member.Id,
                GameTable = table,
                Member = member,

            });
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
