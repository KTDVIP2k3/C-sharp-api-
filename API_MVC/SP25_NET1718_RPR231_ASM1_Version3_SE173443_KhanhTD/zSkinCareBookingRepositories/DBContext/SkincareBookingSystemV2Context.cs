using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using zSkinCareBookingRepositories_.Models;

namespace zSkinCareBookingRepositories_.DBContext;

public partial class SkincareBookingSystemV2Context : DbContext
{
    public SkincareBookingSystemV2Context()
    {
    }

    public SkincareBookingSystemV2Context(DbContextOptions<SkincareBookingSystemV2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<BlogCategory> BlogCategories { get; set; }

    public virtual DbSet<BlogPostCategory> BlogPostCategories { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingDetail> BookingDetails { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<QuizResult> QuizResults { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Therapist> Therapists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

	public static string GetConnectionString(string connectionStringName)
	{
		var config = new ConfigurationBuilder()
			.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
			.AddJsonFile("appsettings.json")
			.Build();

		string connectionString = config.GetConnectionString(connectionStringName);
		return connectionString;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"));

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Blog__3214EC27598912A9");

            entity.ToTable("Blog");

            entity.HasIndex(e => e.StaffId, "IDX_Blog_Staff");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreateAt_DateTime");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.PublishDate).HasColumnType("datetime");
            entity.Property(e => e.StaffId).HasColumnName("Staff_ID");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateAt_DateTime");

            entity.HasOne(d => d.Staff).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Blog__Staff_ID__25518C17");
        });

        modelBuilder.Entity<BlogCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BlogCate__3214EC272996D996");

            entity.ToTable("BlogCategory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreateAt_DateTime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateAt_DateTime");
        });

        modelBuilder.Entity<BlogPostCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BlogPost__3214EC2761601838");

            entity.ToTable("BlogPostCategory");

            entity.HasIndex(e => e.BlogId, "IDX_BlogPostCategory_Blog");

            entity.HasIndex(e => e.BlogCategoryId, "IDX_BlogPostCategory_Category");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BlogCategoryId).HasColumnName("Blog_Category_ID");
            entity.Property(e => e.BlogId).HasColumnName("Blog_ID");
            entity.Property(e => e.CreateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreateAt_DateTime");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.UpdateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateAt_DateTime");

            entity.HasOne(d => d.BlogCategory).WithMany(p => p.BlogPostCategories)
                .HasForeignKey(d => d.BlogCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BlogPostC__Blog___2BFE89A6");

            entity.HasOne(d => d.Blog).WithMany(p => p.BlogPostCategories)
                .HasForeignKey(d => d.BlogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BlogPostC__Blog___2B0A656D");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking__3214EC270A8E6448");

            entity.ToTable("Booking");

            entity.HasIndex(e => e.CustomerId, "IDX_Booking_Customer");

            entity.HasIndex(e => e.ServiceId, "IDX_Booking_Service");

            entity.HasIndex(e => e.TherapistId, "IDX_Booking_Therapist");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookingDate)
                .HasColumnType("datetime")
                .HasColumnName("Booking_Date");
            entity.Property(e => e.CreateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreateAt_DateTime");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.ServiceId).HasColumnName("Service_ID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TherapistId).HasColumnName("Therapist_ID");
            entity.Property(e => e.UpdateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateAt_DateTime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__Custome__787EE5A0");

            entity.HasOne(d => d.Service).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__Service__778AC167");

            entity.HasOne(d => d.Therapist).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TherapistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__Therapi__797309D9");
        });

        modelBuilder.Entity<BookingDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookingD__3214EC2738147313");

            entity.ToTable("BookingDetail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookingId).HasColumnName("Booking_ID");
            entity.Property(e => e.CreateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreateAt_DateTime");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.ServiceId).HasColumnName("Service_ID");
            entity.Property(e => e.UpdateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateAt_DateTime");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingDe__Booki__00200768");

            entity.HasOne(d => d.Service).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookingDe__Servi__7F2BE32F");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC279F410725");

            entity.ToTable("Customer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreateAt_DateTime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Fullname).HasMaxLength(100);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UpdateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateAt_DateTime");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customer__User_I__66603565");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__History__3214EC27856272D8");

            entity.ToTable("History");

            entity.HasIndex(e => e.CustomerId, "IDX_History_Customer");

            entity.HasIndex(e => e.BookingId, "UQ__History__35ABFDE1C40E1080").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookingId).HasColumnName("Booking_ID");
            entity.Property(e => e.CreateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreateAt_DateTime");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.UpdateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateAt_DateTime");

            entity.HasOne(d => d.Booking).WithOne(p => p.History)
                .HasForeignKey<History>(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__History__Booking__0F624AF8");

            entity.HasOne(d => d.Customer).WithMany(p => p.Histories)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__History__Custome__0E6E26BF");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quiz__3214EC27EAD179FF");

            entity.ToTable("Quiz");

            entity.HasIndex(e => e.CustomerId, "IDX_Quiz_Customer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreateAt_DateTime");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateAt_DateTime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Quiz__Customer_I__1AD3FDA4");
        });

        modelBuilder.Entity<QuizResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quiz_Res__3214EC2732A0F4AC");

            entity.ToTable("Quiz_Result");

            entity.HasIndex(e => e.CustomerId, "IDX_QuizResult_Customer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreateAt_DateTime");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.IsUsingAnyProduct).HasColumnName("Is_Using_Any_Product");
            entity.Property(e => e.SkinType)
                .HasMaxLength(50)
                .HasColumnName("Skin_Type");
            entity.Property(e => e.SkinTypeToTreat)
                .HasMaxLength(50)
                .HasColumnName("Skin_Type_To_Treat");
            entity.Property(e => e.UpdateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateAt_DateTime");

            entity.HasOne(d => d.Customer).WithMany(p => p.QuizResults)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Quiz_Resu__Custo__151B244E");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Schedule__3214EC2717510D27");

            entity.ToTable("Schedule");

            entity.HasIndex(e => e.TherapistId, "IDX_Schedule_Therapist");

            entity.HasIndex(e => e.BookingId, "UQ__Schedule__35ABFDE172DCCECF").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookingId).HasColumnName("Booking_ID");
            entity.Property(e => e.CreateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreateAt_DateTime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EndsAt).HasColumnName("Ends_At");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.StartFrom).HasColumnName("Start_From");
            entity.Property(e => e.TherapistId).HasColumnName("Therapist_ID");
            entity.Property(e => e.UpdateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateAt_DateTime");

            entity.HasOne(d => d.Booking).WithOne(p => p.Schedule)
                .HasForeignKey<Schedule>(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Schedule__Bookin__07C12930");

            entity.HasOne(d => d.Therapist).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.TherapistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Schedule__Therap__06CD04F7");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Service__3214EC2721B32CDD");

            entity.ToTable("Service");

            entity.HasIndex(e => e.ServiceCategoryId, "IDX_Service_Category");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreateAt_DateTime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EstimateDuration).HasColumnName("Estimate_Duration");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ServiceCategoryId).HasColumnName("Service_Category_ID");
            entity.Property(e => e.UpdateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateAt_DateTime");

            entity.HasOne(d => d.ServiceCategory).WithMany(p => p.Services)
                .HasForeignKey(d => d.ServiceCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Service__Service__5BE2A6F2");
        });

        modelBuilder.Entity<ServiceCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ServiceC__3214EC27DC20B1DF");

            entity.ToTable("ServiceCategory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreateAt_DateTime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateAt_DateTime");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Staff__3214EC27BF7B9791");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreateAt_DateTime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Fullname).HasMaxLength(100);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UpdateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateAt_DateTime");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Staff)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Staff__User_ID__71D1E811");
        });

        modelBuilder.Entity<Therapist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Therapis__3214EC278049A1E1");

            entity.ToTable("Therapist");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bio).HasMaxLength(500);
            entity.Property(e => e.CreateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreateAt_DateTime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.ExpMonth).HasColumnName("Exp_Month");
            entity.Property(e => e.Fullname).HasMaxLength(100);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Specialization).HasMaxLength(255);
            entity.Property(e => e.UpdateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateAt_DateTime");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Therapists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Therapist__User___6C190EBB");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC27A815F1E3");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CreateAt_DateTime");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.UpdateAtDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateAt_DateTime");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserAccountId).HasName("PK__UserAcco__DA6C70BA25AB925F");

            entity.ToTable("UserAccount");

            entity.HasIndex(e => e.Email, "UQ__UserAcco__A9D1053421F3BEEC").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__UserAcco__C9F284562B78C11E").IsUnique();

            entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");
            entity.Property(e => e.ApplicationCode).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.RequestCode).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
