using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPatientAccount.Models.ModelsView
{
    public class AppointmentView
    {
        public string id { get; set; }
        public string date { get; set; }
        public string fiodoctor { get; set; }
        public string time { get; set; }
        public string office { get; set; }
    }
}
