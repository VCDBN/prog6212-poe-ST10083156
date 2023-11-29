using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace PROG_POE.Models;

public partial class StudentStudyContext : DbContext
{
    public StudentStudyContext()
    {
    }

    public StudentStudyContext(DbContextOptions<StudentStudyContext> options)
        : base(options)
    {
    }
    public List<Module> GetModules(string username) 
    { 
        StudentStudyContext studentStudyContext = new StudentStudyContext();
        return studentStudyContext.Modules.Where(m => m.Username ==username).ToList(); 
    }
    public List<Semester> GetSemesters(string username)
    {
        StudentStudyContext studentStudyContext = new StudentStudyContext();
        return studentStudyContext.Semesters.Where(s => s.Username == username).ToList();
    }
    
    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    => optionsBuilder.UseSqlServer("Data Source=DESKTOP-SF14FEM\\SQLEXPRESS;Initial Catalog=StudentStudy;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Module__A25C5AA63FE98FB6");

            entity.ToTable("Module");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Modules)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__Module__Username__5EBF139D");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Semester__3214EC27D4C55373");

            entity.ToTable("Semester");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.CodeNavigation).WithMany(p => p.Semesters)
                .HasForeignKey(d => d.Code)
                .HasConstraintName("FK__Semester__Code__619B8048");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Semesters)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__Semester__Userna__628FA481");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Users__536C85E5CD5E1973");

            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(128)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
