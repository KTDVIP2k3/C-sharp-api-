using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using zSkinCareBookingRepositories_.DBContext;
using zSkinCareBookingRepositories_.Models;

namespace zSkinCareBookingFE_RazorPage.Pages
{
    public class DetailsModel : PageModel
    {
		private readonly zSkinCareBookingRepositories_.DBContext.SkincareBookingSystemV2Context _context = new SkincareBookingSystemV2Context();

		//public DetailsModel(zSkinCareBookingRepositories_.DBContext.SkincareBookingSystemV2Context context)
  //      {
  //          _context = context;
  //      }

        public Schedule Schedule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FirstOrDefaultAsync(m => m.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }
            else
            {
                Schedule = schedule;
            }
            return Page();
        }
    }
}
