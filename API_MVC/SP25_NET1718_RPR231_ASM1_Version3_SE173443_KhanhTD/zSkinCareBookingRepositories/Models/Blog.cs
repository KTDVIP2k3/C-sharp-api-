using System;
using System.Collections.Generic;

namespace zSkinCareBookingRepositories_.Models;

public partial class Blog
{
    public int Id { get; set; }

    public int StaffId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime PublishDate { get; set; }

    public DateTime? CreateAtDateTime { get; set; }

    public DateTime? UpdateAtDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<BlogPostCategory> BlogPostCategories { get; set; } = new List<BlogPostCategory>();

    public virtual Staff Staff { get; set; } = null!;
}
