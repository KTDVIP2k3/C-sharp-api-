using System;
using System.Collections.Generic;

namespace zSkinCareBookingRepositories_.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public int CustomerId { get; set; }

    public int TherapistId { get; set; }

    public DateTime BookingDate { get; set; }

    public string? Notes { get; set; }

    public string? Status { get; set; }

    public DateTime? CreateAtDateTime { get; set; }

    public DateTime? UpdateAtDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual Customer Customer { get; set; } = null!;

    public virtual History? History { get; set; }

    public virtual Schedule? Schedule { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual Therapist Therapist { get; set; } = null!;
}
