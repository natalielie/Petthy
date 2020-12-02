﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Petthy.Models;
using Petthy.Models.Pet;
using Petthy.Models.Professional;
using Petthy.Models.SmartDevice;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petthy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Professional> Professionals { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetDiaryNote> PetDiaryNotes { get; set; }
        public DbSet<PetMedCardNote> PetMedCardNotes { get; set; }
        public DbSet<SmartDeviceData> SmartDeviceData { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ProfessionalRole> ProfessionalRoles { get; set; }
        public DbSet<ProfessionalSchedule> ProfessionalSchedules { get; set; }

        public DbSet<PetAssignment> PetAssignments { get; set; }
        public DbSet<ProfessionalAppointment> ProfessionalAppointments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PetAssignment>()
               .HasKey(c => new { c.PetId, c.ProfessionalId });

            modelBuilder.Entity<ProfessionalAppointment>()
                .HasKey(c => new { c.ProfessionalId, c.PetId, c.AppointmentDateTime });

            modelBuilder.Entity<ProfessionalSchedule>()
                .HasKey(c => new { c.ProfessionalId, c.Weekday, c.DateTimeBegin, c.DateTimeEnd });

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    ClientId = 1,
                    FirstName = "Tyler",
                    LastName = "Joseph",
                    PhoneNumber = "+40097656789",
                    DateOfBirth = DateTime.Parse("1988-12-01"),
                    Address = "Riverside st, 33b"
                }
            );

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    ClientId = 2,
                    FirstName = "Joshua",
                    LastName = "Dun",
                    PhoneNumber = "+40054776512",
                    DateOfBirth = DateTime.Parse("1988-06-12"),
                    Address = "Riverside st, 33a"
                }
            );
        }
    }
}
