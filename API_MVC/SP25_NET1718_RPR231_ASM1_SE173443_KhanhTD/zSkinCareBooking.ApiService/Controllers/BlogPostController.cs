using Microsoft.AspNetCore.Mvc;
using zSckinCareBookingService.InterfaceServices;
using zSkinCareBookingRepositories.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace zSkinCareBooking.ApiService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostController : ControllerBase
	{
		private readonly IBlogPostService blogPostService;

		public BlogPostController(IBlogPostService blogPostService) => this.blogPostService = blogPostService;
		// GET: api/<BlogPostController>
		[HttpGet]
		public async Task<List<BlogPost>> Get()
		{
			return await blogPostService.GetAllBlogPost();
		}

		// GET api/<BlogPostController>/5
		[HttpGet("{id}")]
		public async Task<BlogPost> GetById(int id)
		{
			return await blogPostService.GetBlogPostById(id);
		}

		[HttpGet("{title}/{content}/{slug}/{metaDescription}")]
		public async Task<List<BlogPost>> Search(string title, string content, string slug, string metaDescription)
		{
			return await blogPostService.Search(title, content, slug, metaDescription);
		}

		// POST api/<BlogPostController>
		[HttpPost]
		public async Task<int> Create([FromBody] BlogPost blogPost)
		{
			return await blogPostService.Create(blogPost);
		}

		// PUT api/<BlogPostController>/5
		[HttpPut("{id}")]
		public async Task<int> Update([FromBody] BlogPost blogPost)
		{
			return await blogPostService.Update(blogPost);
		}

		// DELETE api/<BlogPostController>/5
		[HttpDelete("{id}")]
		public async Task<bool> Delete(int id)
		{
			return await blogPostService.Delete(id);
		}
	}
}
