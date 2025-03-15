﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using zSkinCareBookingRepositories.Models;

namespace zSkinCareBookingRepositories.DBContext;

public partial class SkinCareBookingSystemContext : DbContext
{
    public SkinCareBookingSystemContext()
    {
    }

    public SkinCareBookingSystemContext(DbContextOptions<SkinCareBookingSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BlogCategory> BlogCategories { get; set; }

    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=SkinCareBookingSystem;Persist Security Info=True;User ID=sa;Password=sa123456;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogCategory>(entity =>
        {
            entity.HasKey(e => e.BlogCategoryId).HasName("PK__BlogCate__6BD2DA01100A4DF8");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__BlogPost__AA126018FF0681AE");

            entity.Property(e => e.IsPublished).HasDefaultValue(false);
            entity.Property(e => e.MetaDescription).HasMaxLength(255);
            entity.Property(e => e.PublishedDate).HasColumnType("datetime");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasMany(d => d.Categories).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "BlogPostCategory",
                    r => r.HasOne<BlogCategory>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BlogPostC__Categ__6D0D32F4"),
                    l => l.HasOne<BlogPost>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BlogPostC__PostI__6C190EBB"),
                    j =>
                    {
                        j.HasKey("PostId", "CategoryId").HasName("PK__BlogPost__0B82F3B87AD1DC53");
                        j.ToTable("BlogPostCategories");
                    });
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.ToTable("UserAccount");

            entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");
            entity.Property(e => e.ApplicationCode).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(e => e.EmployeeCode)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.RequestCode).HasMaxLength(50);
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}