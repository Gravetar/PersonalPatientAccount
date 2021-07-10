using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PersonalPatientAccount.Models
{
    public class PatientContext: DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Outpatient_card> Outpatient_cards { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Shedule> Shedules { get; set; }
        public DbSet<ImageModel> Images { get; set; }

        public PatientContext(DbContextOptions<PatientContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
