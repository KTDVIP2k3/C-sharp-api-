using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using zSckinCareBookingService.InterfaceServices;
using zSkinCareBookingRepositories.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace zSkinCareBooking.ApiService.Controllers
{
	[Route("api/blog")]
	[ApiController]
	public class BlogPostController : ControllerBase
	{
		private readonly IBlogPostService blogPostService;

		public BlogPostController(IBlogPostService blogPostService) => this.blogPostService = blogPostService;
		//GET: api/<BlogPostController>
		//[HttpGet]
		//public async Task<List<BlogPost>> GetAll()
		//{
		//	List<BlogPost> blogPosts = await blogPostService.GetAllBlogPosts();
		//	if(blogPosts.IsNullOrEmpty())
		//	{
		//		return await blogPostService.GetAllBlogPosts();
		//	}
		//}
		public static int ReturnNumber()
		{
			return 1;
		}
		[HttpGet("/getAllBlogPost")]
		public async Task<IActionResult> GetAllBlogPost()
		{
			List<BlogPost> blogPosts = await blogPostService.GetAllBlogPosts();
			int i = ReturnNumber();
			return Ok(new
			{
				message = blogPosts.Any() ? "Danh sách blog post!" : "Danh sách trống!",
				data = blogPosts
			});
		}

		[HttpGet("getBlogPostById/{id}")]
		public async Task<IActionResult> GetBlogPostById(int id)
		{
			if (await blogPostService.GetBlogPostById(id) == null)
				return NotFound(new { message = "Blog post này không tồn tại", data = await blogPostService.GetBlogPostById(id)});
			
			return Ok(new { message = "Blog post có tồn tại", data = await blogPostService.GetBlogPostById(id) });
		}

		[HttpGet("search/{title}/{content}/{slug}/{metaDescription}")]
		public async Task<IActionResult> Search(String title, String content, String slug, String metaDescription)
		{
			if(await  blogPostService.Search(title, content, slug, metaDescription) == null)
				return NotFound(new { message = "Không tìm thấy blogpost", data = await blogPostService.Search(title, content, slug, metaDescription), status = StatusCodes.Status204NoContent});
			
			return Ok(new {mesage = "BLog Post", data = await blogPostService.Search(title, content, slug, metaDescription), status = StatusCodes.Status200OK});
		}

		[HttpPost("createBLogPost")]
		public async Task<IActionResult> CreateBlogPost([FromBody] BlogPost blogPost)
		{
			if(await blogPostService.CreateBlogPost(blogPost) > 0)
			{
				return Ok(new { mesage = "Create blog post successfully" });
			}
			
			return BadRequest(new { mesage = "Create blog post unsucessfully" });
		}

		[HttpPut("/updateBlogPost")]
		public async Task<IActionResult> UpdateBlogPost([FromBody] BlogPost blog)
		{
			if (await blogPostService.UpdateBlogPost(blog) > 0)
			{
				return Ok(new { message = "Update blog post successfully" });
			}
			return BadRequest(new { message = "Update blog post fail" });
		}

		[HttpDelete("deleteBlogPost/{blogPostId}")]
		public async Task<IActionResult> DeleteBlogPost(int blogPostId)
		{
			if(await blogPostService.DeleteBlogPostById(blogPostId))
			{
				return Ok(new {message = "Delete blog post by id succesfully"});
			}

			return BadRequest(new {message = "Delete blog post by id fail"});
		}

		//// GET api/<BlogPostController>/5
		//[HttpGet("{id}")]
		//public async Task<BlogPost> GetById(int id)
		//{
		//	return await blogPostService.GetBlogPostById(id);
		//}

		//[HttpGet("{title}/{content}/{slug}/{metaDescription}")]
		//public async Task<List<BlogPost>> Search(string title, string content, string slug, string metaDescription)
		//{
		//	return await blogPostService.Search(title, content, slug, metaDescription);
		//}

		//// POST api/<BlogPostController>
		//[HttpPost]
		//public async Task<int> Create([FromBody] BlogPost blogPost)
		//{
		//	return await blogPostService.Create(blogPost);
		//}

		//// PUT api/<BlogPostController>/5
		//[HttpPut("{id}")]
		//public async Task<int> Update([FromBody] BlogPost blogPost)
		//{
		//	return await blogPostService.Update(blogPost);
		//}

		//// DELETE api/<BlogPostController>/5
		//[HttpDelete("{id}")]
		//public async Task<bool> Delete(int id)
		//{
		//	return await blogPostService.Delete(id);
		//}
	}
}
