using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPatientAccount.Models
{
    public class Doctor
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patrynomic { get; set; }
        public string office { get; set; }
        /// <summary>
        /// Имя Фамилия Отчество
        /// </summary>
        public string FullName { get => $"{surname} {name} {patrynomic}"; }

        public List<int> sheduleid { get; set; }
        public virtual List<Shedule> Shedule { get; set; }
    }
}
