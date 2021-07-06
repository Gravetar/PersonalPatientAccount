using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPatientAccount.Models
{
    public class Shedule
    {
        [Key]
        public int id { get; set; }
        public int dateofweek { get; set; }
        public string time { get; set; }

        public List<int> doctorid { get; set; }
        public virtual List<Doctor> Doctor { get; set; }
    }
}
