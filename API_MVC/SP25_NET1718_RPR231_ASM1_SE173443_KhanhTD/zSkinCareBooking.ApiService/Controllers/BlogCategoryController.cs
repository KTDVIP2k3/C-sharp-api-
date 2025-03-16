using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace zSkinCareBooking.ApiService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogCategoryController : ControllerBase
	{
		[HttpGet ("/api/get")]
		public String Get()
		{
			return "API Blog Category";
		}
	}
}
