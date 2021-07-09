using System.ComponentModel.DataAnnotations;

namespace PersonalPatientAccount.Models.ModelsView
{
    public class CardView
    {
        public string diagnose { get; set; }
        public string namedoctor { get; set; }
        public string surnamedoctor { get; set; }
        public string patronymicdoctor { get; set; }
        public string date { get; set; }
        public string type { get; set; }
    }
}
