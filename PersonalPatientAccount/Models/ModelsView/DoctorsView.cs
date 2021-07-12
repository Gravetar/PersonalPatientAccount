using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPatientAccount.Models.ModelsView
{
    public class DoctorsView
    {
        public string id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string fullname { get => $"{surname} {name} {patronymic}"; }
    }
}
