using System;
using System.Collections.Generic;

namespace zSkinCareBookingRepositories_.Models;

public partial class Service
{
    public int Id { get; set; }

    public int ServiceCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public int Price { get; set; }

    public TimeOnly EstimateDuration { get; set; }

    public DateTime? CreateAtDateTime { get; set; }

    public DateTime? UpdateAtDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ServiceCategory ServiceCategory { get; set; } = null!;
}
