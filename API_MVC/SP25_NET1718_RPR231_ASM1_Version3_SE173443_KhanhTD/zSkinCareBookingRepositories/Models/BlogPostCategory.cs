using System;
using System.Collections.Generic;

namespace zSkinCareBookingRepositories_.Models;

public partial class BlogPostCategory
{
    public int Id { get; set; }

    public int BlogId { get; set; }

    public int BlogCategoryId { get; set; }

    public DateTime? CreateAtDateTime { get; set; }

    public DateTime? UpdateAtDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Blog Blog { get; set; } = null!;

    public virtual BlogCategory BlogCategory { get; set; } = null!;
}
