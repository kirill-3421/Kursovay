using Kursovay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovay
{
    internal class DrivingSchoolDbContext : DbContext
    {
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// подключение к БД
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLlocalDB;Database=DrivingSchoolDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // связь: программа - группы
            modelBuilder.Entity<Group>()
                .HasOne(g => g.TrainingProgram)
                .WithMany(p => p.Groups)
                .HasForeignKey(g => g.TrainingProgramId)
                .OnDelete(DeleteBehavior.Restrict); 

            // связь: группа - студенты
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // связь: группа - занятия
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Group)
                .WithMany(g => g.Lessons)
                .HasForeignKey(l => l.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // связь: автомобиль - занятия
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Car)
                .WithMany(c => c.Lessons)
                .HasForeignKey(l => l.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            // связь: преподаватель - занятия
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Instructor)
                .WithMany(i => i.Lessons)
                .HasForeignKey(l => l.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            // связь:студент - экзамены
            modelBuilder.Entity<Exam>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Exams)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // связь: преподаватель - экзамены
            modelBuilder.Entity<Exam>()
                .HasOne(e => e.Instructor)
                .WithMany(i => i.Exams)
                .HasForeignKey(e => e.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Instructor>()
                .Property(i => i.FullName)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(s => s.FullName)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}

