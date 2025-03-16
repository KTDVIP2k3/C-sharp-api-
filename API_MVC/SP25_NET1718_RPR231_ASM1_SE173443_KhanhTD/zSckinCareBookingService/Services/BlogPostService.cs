using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSckinCareBookingService.InterfaceServices;
using zSkinCareBookingRepositories;
using zSkinCareBookingRepositories.Models;

namespace zSckinCareBookingService.Services
{
	public class BlogPostService : IBlogPostService
	{
		private readonly BlogPostsRepository blogPostsRepository;

		public BlogPostService()
		{
			blogPostsRepository = new BlogPostsRepository();
		}

		public async Task<int> CreateBlogPost(BlogPost blogPost)
		{
			return await blogPostsRepository.CreateAsync(blogPost);
		}

		public async Task<bool> DeleteBlogPost(int id)
		{
			var item = await blogPostsRepository.GetByIdAsync(id);
			if(item != null)
			{
				return await blogPostsRepository.RemoveAsync(item);
			}

			return false;
		}

		public Task<bool> DeleteBlogPostById(int blogPostId)
		{
			return blogPostsRepository.DeleteBlogPostById(blogPostId);
		}

		public async Task<List<BlogPost>> GetAllBlogPosts()
		{
			return await blogPostsRepository.GetAllAsync();
		}

		public async Task<BlogPost> GetBlogPostById(int id)
		{
			return await blogPostsRepository.GetByIdAsync(id);
		}

		public async Task<List<BlogPost>> Search(string title, string content, string slug, string metaDescription)
		{
			return await blogPostsRepository.SearchBlogPost(title, content, slug, metaDescription);
		}

		public Task<int> UpdateBlogPost(BlogPost blogPost)
		{
			return blogPostsRepository.UpdateAsync(blogPost);
		}
		//public async Task<int> Create(BlogPost blogPost)
		//{
		//	return await blogPostsRepository.CreateAsync(blogPost);
		//}

		//public async Task<bool> Delete(int id)
		//{
		//	var item = blogPostsRepository.GetById(id);
		//	if (item != null)
		//	{
		//		return await blogPostsRepository.RemoveAsync(item);
		//	}
		//	return false;
		//}

		//public async Task<List<BlogPost>> GetAllBlogPost()
		//{
		//	return await blogPostsRepository.GetAll();
		//}

		//public async Task<BlogPost> GetBlogPostById(int id)
		//{

		//	return await blogPostsRepository.GetByIdAsync(id);
		//}

		//public async Task<List<BlogPost>> Search(string title, string content, string slug, string metaDescription)
		//{
		//	return await blogPostsRepository.SearchBlogPost(title, content, slug, metaDescription);
		//}

		//public Task<int> Update(BlogPost blogPost)
		//{
		//	return blogPostsRepository.UpdateAsync(blogPost);
		//}
	}
}
