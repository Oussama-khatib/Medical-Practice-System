using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using App.core;
namespace App.Infrastructure
{
    internal class AppDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Connections.sqlConnectionString);
        }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<PatientDisease> Patient_Disease { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
