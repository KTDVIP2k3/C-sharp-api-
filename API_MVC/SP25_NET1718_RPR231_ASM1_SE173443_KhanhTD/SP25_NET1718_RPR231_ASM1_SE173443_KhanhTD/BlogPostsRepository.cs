using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zSkinCareBookingRepositories.Base;
using zSkinCareBookingRepositories.Models;

namespace zSkinCareBookingRepositories
{
	public class BlogPostsRepository : GenericRepository<BlogPost>
	{
		public BlogPostsRepository() { }

		public async Task<List<BlogPost>> GetAll()
		{
			return await _context.BlogPosts.Include(b => b.Categories).ToListAsync();
		}

		public async Task<List<BlogPost>> SearchBlogPost(string title, string content, string slug, string metaDescription)
		{
			return await _context.BlogPosts
				.Include(b => b.Categories)
				.Where(b => string.IsNullOrEmpty(title) || b.Title.Contains(title) ||
				b.Title.Equals(title) || string.IsNullOrEmpty(content) || b.Content.Contains(content) ||
				b.Content.Equals(content) || string.IsNullOrEmpty(slug) || b.Slug.Contains(slug) || b.Slug.Equals(slug) ||
				 string.IsNullOrEmpty(metaDescription) || b.MetaDescription.Contains(metaDescription) ||
				b.MetaDescription.Equals(metaDescription)).ToListAsync();
		}
	}
}
