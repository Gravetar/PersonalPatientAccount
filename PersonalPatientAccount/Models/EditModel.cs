using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPatientAccount.Models
{
    public class EditModel
    {
        [Required(ErrorMessage = "Не указано id")]
        public string id { get; set; }

        [Required(ErrorMessage = "Не указано Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана Фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Не указано Отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Не указан Номер полиса")]
        public string Numberpolicy { get; set; }

        [Required(ErrorMessage = "Не указан паспорт")]
        public string Numberpassport { get; set; }

        [Required(ErrorMessage = "Не указан телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Не указана почта(email)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указана дата рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Dateofbirth { get; set; }
    }
}
