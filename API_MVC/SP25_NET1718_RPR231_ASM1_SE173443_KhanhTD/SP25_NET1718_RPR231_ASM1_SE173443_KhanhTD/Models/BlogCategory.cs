﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace zSkinCareBookingRepositories.Models;

public partial class BlogCategory
{
    public int BlogCategoryId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

	public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
}