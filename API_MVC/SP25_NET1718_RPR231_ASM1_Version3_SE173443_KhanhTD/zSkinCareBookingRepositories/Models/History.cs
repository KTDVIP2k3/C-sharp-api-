using System;
using System.Collections.Generic;

namespace zSkinCareBookingRepositories_.Models;

public partial class History
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int BookingId { get; set; }

    public DateTime Date { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreateAtDateTime { get; set; }

    public DateTime? UpdateAtDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
