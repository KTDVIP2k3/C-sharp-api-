using System;
using System.Collections.Generic;

namespace zSkinCareBookingRepositories_.Models;

public partial class Staff
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Fullname { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateTime? CreateAtDateTime { get; set; }

    public DateTime? UpdateAtDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual User User { get; set; } = null!;
}
