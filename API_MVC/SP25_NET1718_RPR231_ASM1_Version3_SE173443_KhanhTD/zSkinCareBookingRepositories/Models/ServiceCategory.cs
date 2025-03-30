﻿using System;
using System.Collections.Generic;

namespace zSkinCareBookingRepositories_.Models;

public partial class ServiceCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreateAtDateTime { get; set; }

    public DateTime? UpdateAtDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
