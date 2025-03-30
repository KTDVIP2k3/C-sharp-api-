using System;
using System.Collections.Generic;

namespace zSkinCareBookingRepositories_.Models;

public partial class Schedule
{
    public int Id { get; set; }

    public int TherapistId { get; set; }

    public int BookingId { get; set; }

    public DateTime Date { get; set; }

    public TimeOnly StartFrom { get; set; }

    public TimeOnly EndsAt { get; set; }

    public DateTime? CreateAtDateTime { get; set; }

    public DateTime? UpdateAtDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Therapist Therapist { get; set; } = null!;
}
