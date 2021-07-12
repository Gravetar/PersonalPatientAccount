using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPatientAccount.Models
{
    public class Shedule
    {
        [Key]
        public int id { get; set; }
        public string dateofweek { get; set; }
        public string time { get; set; }

        [ForeignKey("Doctor")]
        public int docotorid { get; set; }
        public Doctor Doctor { get; set; }
    }
}
