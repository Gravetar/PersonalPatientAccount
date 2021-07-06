using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPatientAccount.Models
{
    public class Appointment
    {
        [Key]
        public int id { get; set; }
        public string date { get; set; }
        public string time { get; set; }

        [ForeignKey("Doctor")]
        public int docotorid { get; set; }
        public Doctor Doctor { get; set; }
        [ForeignKey("Patient")]
        public int patientid { get; set; }
        public Doctor Patient { get; set; }
    }
}
