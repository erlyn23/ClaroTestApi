using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ClaroTest.Domain.Models
{
    public partial class ClaroTestDbContext : DbContext
    {
        public ClaroTestDbContext()
        {
        }

        public ClaroTestDbContext(DbContextOptions<ClaroTestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClassRoom> ClassRooms { get; set; }
        public virtual DbSet<ClassRoomAssignment> ClassRoomAssignments { get; set; }
        public virtual DbSet<DaysOfWeek> DaysOfWeeks { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<ClassRoom>(entity =>
            {
                entity.HasIndex(e => e.Code, "Idx_ClassRooms_Code")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClassRoomAssignment>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.TeacherId, e.StudentId })
                    .HasName("Pk_ClassRoomAssignments");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.ClassRoom)
                    .WithMany(p => p.ClassRoomAssignments)
                    .HasForeignKey(d => d.ClassRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_ClassRoomAssignments_ClassRoom");

                entity.HasOne(d => d.DayOfWeek)
                    .WithMany(p => p.ClassRoomAssignments)
                    .HasForeignKey(d => d.DayOfWeekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_ClassRoomAssignments_DaysOfWeek");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.ClassRoomAssignments)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_ClassRoomAssignments_Students");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.ClassRoomAssignments)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_ClassRoomAssignments_Teachers");
            });

            modelBuilder.Entity<DaysOfWeek>(entity =>
            {
                entity.ToTable("DaysOfWeek");

                entity.HasIndex(e => e.Day, "Idx_DaysOfWeek_Daye")
                    .IsUnique();

                entity.Property(e => e.Day)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(e => e.Email, "Idx_Students_Email")
                    .IsUnique();

                entity.HasIndex(e => e.Enrollment, "Idx_Students_Enrollment")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "Idx_Students_Phone")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Enrollment)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasIndex(e => e.Email, "Idx_Teachers_Email")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "Idx_Teachers_Phone")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
