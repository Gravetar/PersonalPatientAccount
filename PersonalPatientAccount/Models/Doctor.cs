using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("Position")]
        public int positionid { get; set; }
        public Position Position { get; set; }
    }
}
