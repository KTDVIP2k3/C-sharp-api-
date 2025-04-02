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
    public class IndexModel : PageModel
    {
		private readonly zSkinCareBookingRepositories_.DBContext.SkincareBookingSystemV2Context _context = new SkincareBookingSystemV2Context();

		//public IndexModel(zSkinCareBookingRepositories_.DBContext.SkincareBookingSystemV2Context context)
  //      {
  //          _context = context;
  //      }

        public IList<Schedule> Schedule { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Schedule = await _context.Schedules
                .Include(s => s.Booking)
                .Include(s => s.Therapist).ToListAsync();
        }
    }
}
