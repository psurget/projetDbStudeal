using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjetStudeal_Final.Models
{
    public partial class DBStudealContext : DbContext
    {
        public DBStudealContext()
        {
        }

        public DBStudealContext(DbContextOptions<DBStudealContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MeetingRequest> MeetingRequest { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<TimeSlot> TimeSlot { get; set; }
        public virtual DbSet<Tutoring> Tutoring { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=DBStudeal;User ID=student;Password=cgodin2018");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeetingRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.TimeSlotId).HasColumnName("TimeSlotID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.MeetingRequest)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentID_MeetingRequest");

                entity.HasOne(d => d.TimeSlot)
                    .WithMany(p => p.MeetingRequest)
                    .HasForeignKey(d => d.TimeSlotId)
                    .HasConstraintName("FK_TimeSlotID_MeetingRequest");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UQ_UserName")
                    .IsUnique();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsTutor).HasColumnName("isTutor");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.Property(e => e.TimeSlotId).HasColumnName("TimeSlotID");

                entity.Property(e => e.Day)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TutoringId).HasColumnName("TutoringID");

                entity.HasOne(d => d.Tutoring)
                    .WithMany(p => p.TimeSlot)
                    .HasForeignKey(d => d.TutoringId)
                    .HasConstraintName("FK_TutoringID_TimeSlot");
            });

            modelBuilder.Entity<Tutoring>(entity =>
            {
                entity.Property(e => e.TutoringId).HasColumnName("TutoringID");

                entity.Property(e => e.CreationDate).HasColumnType("date");

                entity.Property(e => e.Subject)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TutorId).HasColumnName("TutorID");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.Tutoring)
                    .HasForeignKey(d => d.TutorId)
                    .HasConstraintName("FK_TutorID_Tutoring");
            });
        }
    }
}
