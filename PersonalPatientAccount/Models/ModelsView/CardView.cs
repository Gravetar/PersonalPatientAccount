using System.ComponentModel.DataAnnotations;

namespace PersonalPatientAccount.Models.ModelsView
{
    public class CardView
    {
        public string Diagnose { get; set; }
        public string NameDoctor { get; set; }
        public string SurnameDoctor { get; set; }
        public string PatronymicDoctor { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
    }
}
