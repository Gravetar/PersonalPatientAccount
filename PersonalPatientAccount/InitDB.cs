using PersonalPatientAccount.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPatientAccount
{
    public class InitDB
    {
        public static void Initialize(PatientContext context)
        {
            if (!context.Patients.Any())
            {
                context.Patients.AddRange(
                    new Patient
                    {
                        name = "Иван",
                        surname= "Иванов",
                        patronymic = "Иванович",
                        password = "password",
                        dateofbirth = new DateTime(2000, 01, 07),
                        numberpolicy = "1234567891012345",
                        numberpassport = "1234567890",
                        email = "Ivan@mail.ru",
                        phone = "89111111111"
                    },
                    new Patient
                    {
                        name = "Петр",
                        surname = "Петров",
                        patronymic = "Петрович",
                        password = "qwerty123",
                        dateofbirth = new DateTime(1993, 06, 13),
                        numberpolicy = "1234589540812345",
                        numberpassport = "1238593890",
                        email = "PetrPetrov@mail.ru",
                        phone = "89111865711"
                    },
                    new Patient
                    {
                        name = "Василий",
                        surname = "Васильев",
                        patronymic = "Васильевич",
                        password = "112233",
                        dateofbirth = new DateTime(1999, 05, 01),
                        numberpolicy = "1234595036485345",
                        numberpassport = "1231285890",
                        email = "1999Vas@mail.ru",
                        phone = "89119175111"
                    }
                );
                context.SaveChanges();
            }

            if (!context.Positions.Any())
            {
                context.Positions.AddRange(
                    new Position
                    {
                        name = "Терапевт"
                    },
                    new Position
                    {
                        name = "Лор"
                    },
                    new Position
                    {
                        name = "Хирург"
                    }
                );
                context.SaveChanges();
            }

            if (!context.Doctors.Any())
            {
                context.Doctors.AddRange(
                    new Doctor
                    {
                        name="Андрей",
                        surname = "Андреев",
                        patrynomic = "Андреевич",
                        office = "20",
                        positionid = 1
                    },
                    new Doctor
                    {
                        name = "Максим",
                        surname = "Максимов",
                        patrynomic = "Максимович",
                        office = "43а",
                        positionid = 1
                    },
                    new Doctor
                    {
                        name = "Александр",
                        surname = "Александров",
                        patrynomic = "Александрович",
                        office = "51б",
                        positionid = 2
                    },
                    new Doctor
                    {
                        name = "Алексей",
                        surname = "Алексеев",
                        patrynomic = "Алексеевич",
                        office = "34",
                        positionid = 3
                    },
                    new Doctor
                    {
                        name = "Антон",
                        surname = "Антонов",
                        patrynomic = "Антонович",
                        office = "11",
                        positionid = 2
                    }
                );
                context.SaveChanges();
            }

            if (!context.Appointments.Any())
            {
                context.Appointments.AddRange(
                    new Appointment
                    {
                        date = "2021-07-14",
                        time = "12:00",
                        docotorid = 1,
                        patientid = 1
                    },
                    new Appointment
                    {
                        date = "2021-07-17",
                        time = "16:00",
                        docotorid = 1,
                        patientid = 1
                    },
                    new Appointment
                    {
                        date = "2021-07-15",
                        time = "13:20",
                        docotorid = 1,
                        patientid = 2
                    },
                    new Appointment
                    {
                        date = "2021-07-16",
                        time = "17:00",
                        docotorid = 2,
                        patientid = 2
                    },
                    new Appointment
                    {
                        date = "2021-07-23",
                        time = "18:00",
                        docotorid = 1,
                        patientid = 1
                    }
                );
                context.SaveChanges();
            }

            if (!context.Outpatient_cards.Any())
            {
                context.Outpatient_cards.AddRange(
                    new Outpatient_card
                    {
                        formulation = "Хронический апикальный периодонтит",
                        date = "2020-06-03",
                        type = "Впервые устновленное хроническое",
                        patientid = 1,
                        docotorid = 1
                    },
                    new Outpatient_card
                    {
                        formulation = "Хронический поверхностный гастрит",
                        date = "2019-12-06",
                        type = "Ранее устновленное хроническое",
                        patientid = 1,
                        docotorid = 1
                    },
                    new Outpatient_card
                    {
                        formulation = "Прогрессирующая энцефалопатия",
                        date = "2019-09-09",
                        type = "Острое",
                        patientid = 2,
                        docotorid = 2
                    },
                    new Outpatient_card
                    {
                        formulation = "Острый бронхит",
                        date = "2019-08-31",
                        type = "Острое",
                        patientid = 3,
                        docotorid = 3
                    },
                    new Outpatient_card
                    {
                        formulation = "Железодифицитная анемия неуточечная",
                        date = "2019-07-30",
                        type = "Ранее устновленное хроническое",
                        patientid = 3,
                        docotorid = 5
                    }
                );
                context.SaveChanges();
            }

            if (!context.Shedules.Any())
            {
                context.Shedules.AddRange(
                    new Shedule
                    {
                        dateofweek = "Пн",
                        time = "10:00",
                        docotorid = 1
                    },
                    new Shedule
                    {
                        dateofweek = "Пн",
                        time = "11:30",
                        docotorid = 1
                    },
                    new Shedule
                    {
                        dateofweek = "Пн",
                        time = "13:00",
                        docotorid = 1
                    },
                    new Shedule
                    {
                        dateofweek = "Вт",
                        time = "16:00",
                        docotorid = 1
                    },
                    new Shedule
                    {
                        dateofweek = "Вт",
                        time = "17:30",
                        docotorid = 1
                    },
                    new Shedule
                    {
                        dateofweek = "Вт",
                        time = "18:10",
                        docotorid = 1
                    },
                    new Shedule
                    {
                        dateofweek = "Ср",
                        time = "11:00",
                        docotorid = 1
                    },
                    new Shedule
                    {
                        dateofweek = "Ср",
                        time = "12:20",
                        docotorid = 1
                    },
                    new Shedule
                    {
                        dateofweek = "Ср",
                        time = "14:00",
                        docotorid = 1
                    },
                    new Shedule
                    {
                        dateofweek = "Чт",
                        time = "15:00",
                        docotorid = 1
                    },
                    new Shedule
                    {
                        dateofweek = "Чт",
                        time = "16:20",
                        docotorid = 1
                    },
                    new Shedule
                    {
                        dateofweek = "Чт",
                        time = "17:10",
                        docotorid = 1
                    },
                    new Shedule
                    {
                        dateofweek = "Пт",
                        time = "08:00",
                        docotorid = 1
                    },
                    new Shedule
                    {
                        dateofweek = "Пт",
                        time = "09:00",
                        docotorid = 1
                    },
                    new Shedule
                    {
                        dateofweek = "Пт",
                        time = "11:30",
                        docotorid = 1
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
