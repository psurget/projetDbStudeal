using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetStudeal_Final.Models
{
    public class DBStudealContext : DbContext
    {
        public DBStudealContext()
        {

        }

        public DBStudealContext(DbContextOptions<DBStudealContext> options): base(options)
        {
            AddDataToDatabase();
        }

        public virtual DbSet<MeetingRequest> MeetingRequest { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<TimeSlot> TimeSlot{ get; set; }
        public virtual DbSet<Tutoring> Tutoring { get; set; }
/*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=DBStudeal;User ID=student;Password=cgodin2018");
            }
        }*/

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

        public void AddDataToDatabase()
        {
            //remplir la base avec un jeu de test
            Database.EnsureCreated();
            //verifier s'il y a des données existants
            if (Member.Any() || Tutoring.Any())
            {
                return;
            }
            //ajout des étudiants
            Member m1 = new Member()
            {
                LastName = "Beaudoin",
                FirstName = "Guillaume",
                UserName = "beaudoin.guillaume",
                Password= "12345",
                IsTutor=0,
            };
            Member m2 = new Member()
            {
                LastName = "Lamarche",
                FirstName = "Georges",
                UserName = "lamarche.georges",
                Password = "12345",
                IsTutor = 1,
            };
            Member m3 = new Member()
            {
                LastName = "Petitpas",
                FirstName = "Pierre",
                UserName = "petitpas.pierre",
                Password = "5678",
                IsTutor = 1,
            };
            Add(m1);
            Add(m2);
            Add(m3);
            SaveChanges();
            //ajout des cours
            Tutoring t1 = new Tutoring()
            {
                Subject = "ASP NET Core",
                CreationDate= new DateTime(2018,11,15)
            };
            Tutoring t2 = new Tutoring()
            {
                Subject = "ASP NET Core",
                CreationDate = new DateTime(2018,9,12)
            };
            Tutoring t3 = new Tutoring()
            {
                Subject = "Java EE",
                CreationDate = new DateTime(2018,10,3)
            };

            Add(t1);
            Add(t2);
            Add(t3);
            SaveChanges();
        }

    }
}
