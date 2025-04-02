using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zSkinCareBookingRepositories_.DBContext;
using zSkinCareBookingRepositories_.Models;

namespace zSkinCareBookingFE_RazorPage.Pages
{
    public class EditModel : PageModel
    {
		private readonly zSkinCareBookingRepositories_.DBContext.SkincareBookingSystemV2Context _context = new SkincareBookingSystemV2Context();

		//public EditModel(zSkinCareBookingRepositories_.DBContext.SkincareBookingSystemV2Context context)
  //      {
  //          _context = context;
  //      }

        [BindProperty]
        public Schedule Schedule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule =  await _context.Schedules.FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }
            Schedule = schedule;
           ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id");
           ViewData["TherapistId"] = new SelectList(_context.Therapists, "Id", "Fullname");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(Schedule.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }
}
