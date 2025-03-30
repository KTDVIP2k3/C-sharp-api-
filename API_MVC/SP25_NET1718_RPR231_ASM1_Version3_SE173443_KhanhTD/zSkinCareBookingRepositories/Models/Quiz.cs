using System;
using System.Collections.Generic;

namespace zSkinCareBookingRepositories_.Models;

public partial class Quiz
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateAtDateTime { get; set; }

    public DateTime? UpdateAtDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
