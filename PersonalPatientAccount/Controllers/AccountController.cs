using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using PersonalPatientAccount.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PersonalPatientAccount.Controllers
{
    public class AccountController : Controller
    {
        private PatientContext _context;
        public AccountController(PatientContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Patient patient = await _context.Patients.FirstOrDefaultAsync(p => p.email == model.Email);
                if (patient == null)
                {
                    // добавляем пользователя в бд
                    patient = new Patient
                    {
                        email = model.Email,
                        password = model.Password,
                        name = model.Name,
                        surname = model.Surname,
                        patronymic = model.Patronymic,
                        dateofbirth = DateTime.ParseExact(model.Dateofbirth, "yyyy-M-dd", CultureInfo.InvariantCulture),
                        numberpolicy = model.Numberpolicy,
                        numberpassport = model.Numberpassport,
                        phone = model.Phone
                    };

                    _context.Patients.Add(patient);
                    await _context.SaveChangesAsync();

                    await Authenticate(patient); // аутентификация

                    return RedirectToAction("Index", "Profile");
                }
                else
                    ModelState.AddModelError("", "Пользователь с такой почтой уже зарегистрирован");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Patient patient = await _context.Patients.FirstOrDefaultAsync(p => p.email == model.Email && p.password == model.Password);
                if (patient != null)
                {
                    await Authenticate(patient); // аутентификация

                    return RedirectToAction("Index", "Profile");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(Patient patient)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, patient.email)
            };

            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //[HttpGet]
        //public IActionResult Edit()
        //{
        //    string useremail = User.Identity.Name;

        //    Patient patient = _context.Patients.FirstOrDefault(p => p.email.Equals(useremail));

        //    EditModel model = new EditModel();
        //    model.Surname = patient.surname;
        //    model.Name = patient.name;
        //    model.Patronymic = patient.patronymic;
        //    model.Dateofbirth = patient.dateofbirth;
        //    model.Numberpolicy = patient.numberpolicy;
        //    model.Numberpassport = patient.numberpassport;
        //    model.Email = patient.email;
        //    model.Phone = patient.phone;

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Edit(EditModel patientprofile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string useremail = User.Identity.Name;

        //        Patient patient = _context.patients.FirstOrDefault(p => p.email.Equals(useremail));


        //        patient.name = patientprofile.Name;
        //        patient.surname = patientprofile.Surname;
        //        patient.email = patientprofile.Email;

        //        patient.surname = patientprofile.Surname;
        //        patient.name = patientprofile.Name;
        //        patient.patronymic = patientprofile.Patronymic;
        //        patient.dateofbirth = patientprofile.Dateofbirth;
        //        patient.numberpolicy = patientprofile.Numberpolicy;
        //        patient.numberpassport = patientprofile.Numberpassport;
        //        patient.email = patientprofile.Email;
        //        patient.phone = patientprofile.Phone;

        //        _context.Entry(patient).State = EntityState.Modified;

        //        _context.SaveChanges();

        //        return RedirectToAction("Index", "Profile");
        //    }

        //    return View(patientprofile);
        //}

        /// <summary>
        /// Получение текущего пользователя
        /// </summary>
        /// <returns></returns>
        protected Patient CurrentPatient()
        {
            var val = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            if (val == null)
                return null;
            Patient patient = _context.Patients.FirstOrDefault(p => p.email == val);
            return patient;
        }
    }
}
