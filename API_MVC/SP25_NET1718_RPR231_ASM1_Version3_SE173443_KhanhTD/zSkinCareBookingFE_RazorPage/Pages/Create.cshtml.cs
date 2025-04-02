using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using zSkinCareBookingRepositories_.DBContext;
using zSkinCareBookingRepositories_.Models;

namespace zSkinCareBookingFE_RazorPage.Pages
{
    public class CreateModel : PageModel
    {
        private readonly zSkinCareBookingRepositories_.DBContext.SkincareBookingSystemV2Context _context = new SkincareBookingSystemV2Context();

        //public CreateModel(zSkinCareBookingRepositories_.DBContext.SkincareBookingSystemV2Context context)
        //{
        //    _context = context;
        //}

        public IActionResult OnGet()
        {
        ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id");
        ViewData["TherapistId"] = new SelectList(_context.Therapists, "Id", "Fullname");
            return Page();
        }

        [BindProperty]
        public Schedule Schedule { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Schedules.Add(Schedule);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
