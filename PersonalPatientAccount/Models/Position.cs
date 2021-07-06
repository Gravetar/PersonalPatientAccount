using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPatientAccount.Models
{
    public class Position
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }

        public List<Doctor> Doctor { get; set; }
    }
}
