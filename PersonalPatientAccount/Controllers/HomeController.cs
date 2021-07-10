using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonalPatientAccount.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Globalization;
using PersonalPatientAccount.Models.ModelsView;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;

namespace PersonalPatientAccount.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class HomeController : Controller
    {
        IWebHostEnvironment _appEnvironment;

        PatientContext db;
        public HomeController(PatientContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }

        [HttpPost("EditPatient")]
        public IActionResult Edit([FromBody] EditModel user)
        {
            if (ModelState.IsValid)
            {
                string useremail = User.Identity.Name;

                Patient patient = db.Patients.FirstOrDefault(p => p.id == Int32.Parse(user.id));

                patient.surname = user.Surname;
                patient.name = user.Name;
                patient.patronymic = user.Patronymic;
                patient.dateofbirth = user.Dateofbirth;
                patient.numberpolicy = user.Numberpolicy;
                patient.numberpassport = user.Numberpassport;
                patient.email = user.Email;
                patient.phone = user.Phone;

                db.Entry(patient).State = EntityState.Modified;

                var result = db.SaveChanges();
                return Ok(result);
            }

            return BadRequest("Не удалось изменить данные");
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        //POST: api/Patient/Register
        public IActionResult RegisterPatient([FromBody] Models.ModelsView.RegisterModel model)
        {
            var patient = new Patient
            {
                email = model.Email,
                password = model.Password,
                name = model.Name,
                surname = model.Surname,
                patronymic = model.Patronymic,
                dateofbirth = DateTime.ParseExact(model.Dateofbirth, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                numberpolicy = model.Numberpolicy,
                numberpassport = model.Numberpassport,
                phone = model.Phone

            };

            try
            {
                db.Patients.Add(patient);
                var result = db.SaveChanges();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Возвращает текущего пользователя
        /// </summary>
        /// <returns></returns>
        protected int CurrentUserId()
        {
            return int.Parse(User.Claims.FirstOrDefault(p => p.Type == "Id").Value);
        }

        /// <summary>
        /// Получение текущего пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUser")]
        [Authorize]
        public IActionResult CurrentPatient()
        {
            string PatientId = User.Claims.First(c => c.Type == "Id").Value;
            var patient = db.Patients.FirstOrDefault(p => p.id == Int32.Parse(PatientId));
            var response = new
            {
                id = patient.id,
                name = patient.name,
                surname = patient.surname,
                patronymic = patient.patronymic,
                dateofbirth = patient.dateofbirth.ToString("yyyy-MM-dd"),
                numberpolicy = patient.numberpolicy,
                numberpassport = patient.numberpassport,
                phone = patient.phone,
                email = patient.email,
                password = patient.password
            };

            return Json(response);
        }

        /// <summary>
        /// Получение токена доступа (Авторизация)
        /// </summary>
        /// <param name="username">Имя учетной записи (Email)</param>
        /// <param name="password">Пароль учетной записи</param>
        /// <returns>Токен доступа</returns>
        [HttpPost("Auth")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel user)
        {
            var identity = GetIdentity(user.Email, user.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
        }

        /// <summary>
        /// Вывод всех записей амбулаторной карты
        /// </summary>
        /// <param name="id">id пациента</param>
        /// <returns></returns>
        [HttpGet("GetRecords")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<CardView>), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        public IActionResult GetRecords()
        {
            int patientid = CurrentUserId();
            var card_view = new List<CardView>();
            var cards = db.Outpatient_cards.Where(p => p.patientid == patientid).ToList();
            foreach (var item in cards)
            {
                string _formulation = db.Outpatient_cards.FirstOrDefault(p => p.patientid == item.patientid).formulation;
                string date = db.Outpatient_cards.FirstOrDefault(p => p.patientid == item.patientid).date;
                string _type = db.Outpatient_cards.FirstOrDefault(p => p.patientid == item.patientid).type;
                Doctor doctor = db.Doctors.FirstOrDefault(p => p.id == item.docotorid);

                card_view.Add(new CardView()
                {
                    diagnose = _formulation,
                    namedoctor = doctor.name[0] + ".",
                    surnamedoctor = doctor.surname,
                    patronymicdoctor = doctor.patrynomic[0] + ".",
                    date = date,
                    type = _type
                });
            }
            return Ok(card_view ?? new List<CardView>());
        }

        /// <summary>
        /// Вывод всех записей амбулаторной карты
        /// </summary>
        /// <param name="id">id пациента</param>
        /// <returns></returns>
        [HttpGet("GetAppointments")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<CardView>), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        [Authorize]
        public IActionResult GetAppointments()
        {
            int patientid = CurrentUserId();
            var appointment_view = new List<AppointmentView>();
            var appointments = db.Appointments.Where(p => p.patientid == patientid).ToList();
            foreach (var item in appointments)
            {
                string _date = db.Appointments.FirstOrDefault(p => p.patientid == item.patientid && p.id == item.id).date;
                string _FIOdoctor = db.Doctors.FirstOrDefault(p => p.id == item.docotorid).FullName;
                string _time = db.Appointments.FirstOrDefault(p => p.patientid == item.patientid && p.id == item.id).time;
                string _office = db.Doctors.FirstOrDefault(p => p.id == item.docotorid).office;

                appointment_view.Add(new AppointmentView()
                {
                    id = item.id.ToString(),
                    date = _date,
                    fiodoctor = _FIOdoctor,
                    time = _time,
                    office = _office
                });
            }
            return Ok(appointment_view ?? new List<AppointmentView>());
        }

        /// <summary>
        /// Удалить запись к врачу у текущего пациента по айди
        /// </summary>
        /// <param name="id">id пациента</param>
        /// <returns></returns>
        [HttpDelete("RemoveAppointments/{id}")]
        [DisableRequestSizeLimit]
        public IActionResult RemoveAppointments(int id)
        {
            Appointment appointment = db.Appointments.FirstOrDefault(a => a.id == id);
            if (appointment != null)
            {
                db.Appointments.Remove(appointment);
                db.SaveChanges();
            }

            return Ok(appointment);
        }

        /// <summary>
        /// Аутендификация пользователя
        /// </summary>
        /// <param name="username">Email/</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        private ClaimsIdentity GetIdentity(string username, string password)
        {
            Patient patient = db.Patients.FirstOrDefault(p => p.email == username && p.password == password);
            if (patient != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, patient.email),
                    new Claim("Id", patient.id.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        /// <summary>
        /// Вывод пациента по айди
        /// </summary>
        /// <param name="id">id Пациента</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [DisableRequestSizeLimit]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), 400)]
        public Patient GetPatient(int id)
        {
            Patient patient = db.Patients.FirstOrDefault(x => x.id == id);
            return patient;
        }

        /// <summary>
        /// Получение социальной информации пациента
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSocialInfo")]
        [Authorize]
        public IActionResult GetSocialInfo()
        {
            string PatientId = User.Claims.First(c => c.Type == "Id").Value;
            var patient = db.Patients.FirstOrDefault(p => p.id == Int32.Parse(PatientId));
            var response = new
            {
                patientid = patient.id.ToString(),
                pationtfio = patient.fullname,
                age = int.Parse(DateTime.Now.Date.ToString("yyyy")) - int.Parse(patient.dateofbirth.ToString("yyyy")),
                numberpolicy = patient.numberpolicy,
                numberpassport = patient.numberpassport
            };

            return Json(response);
        }

        [HttpPost("UploadImage")]
        public IActionResult UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    fileName = CurrentUserId().ToString() +".png";
                    string fullPath = Path.Combine(_appEnvironment.WebRootPath + "/AccountImages", fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    ImageModel fileUp = new ImageModel {
                        name = file.FileName, 
                        path = "/AccountImages/" + fileName,
                        patientid = CurrentUserId(),
                    };
                    db.Images.Add(fileUp);
                    db.SaveChanges();
                }
                return Json("OK");
            }
            catch (System.Exception ex)
            {
                return Json("Failed");
            }
        }

    }
}
