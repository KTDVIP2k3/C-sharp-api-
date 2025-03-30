using System;
using System.Collections.Generic;

namespace zSkinCareBookingRepositories_.Models;

public partial class QuizResult
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string? SkinType { get; set; }

    public string? Gender { get; set; }

    public int Age { get; set; }

    public bool IsUsingAnyProduct { get; set; }

    public string? SkinTypeToTreat { get; set; }

    public DateTime? CreateAtDateTime { get; set; }

    public DateTime? UpdateAtDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
