using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using zSkinCareBookingRepositories.Models;

namespace zSckinCareBookingService.InterfaceServices
{
	public interface IBlogPostService 
	{
		Task<int> CreateBlogPost(BlogPost blogPost);

		Task<bool> DeleteBlogPost(int id);

		Task<List<BlogPost>> GetAllBlogPosts();

		Task<BlogPost> GetBlogPostById(int id);

		Task<List<BlogPost>> Search(string title, string content, string slug, string metaDescription);

		Task<int> UpdateBlogPost(BlogPost blogPost);

		Task<bool> DeleteBlogPostById(int blogPostId);
	}
}
