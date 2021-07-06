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

        public PatientContext(DbContextOptions<PatientContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
