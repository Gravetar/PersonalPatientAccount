using System.ComponentModel.DataAnnotations;

namespace PersonalPatientAccount.Models
{
    public class RegisterModel
    {
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
        public string Dateofbirth { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
