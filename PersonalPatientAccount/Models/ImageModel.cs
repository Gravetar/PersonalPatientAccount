using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPatientAccount.Models
{
    public class ImageModel
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }

        [ForeignKey("Patient")]
        public int patientid { get; set; }
        public Patient Patient { get; set; }
    }
}
