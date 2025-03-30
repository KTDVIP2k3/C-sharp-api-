using System;
using System.Collections.Generic;

namespace zSkinCareBookingRepositories_.Models;

public partial class Customer
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Fullname { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public DateTime? CreateAtDateTime { get; set; }

    public DateTime? UpdateAtDateTime { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<QuizResult> QuizResults { get; set; } = new List<QuizResult>();

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    public virtual User User { get; set; } = null!;
}
