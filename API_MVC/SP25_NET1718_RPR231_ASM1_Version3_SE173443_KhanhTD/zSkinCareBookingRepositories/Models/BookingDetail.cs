using System;
using System.Collections.Generic;

namespace zSkinCareBookingRepositories_.Models;

public partial class BookingDetail
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public int BookingId { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreateAtDateTime { get; set; }

    public DateTime? UpdateAtDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
