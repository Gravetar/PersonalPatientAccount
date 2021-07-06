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

namespace PersonalPatientAccount.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class HomeController : Controller
    {
        PatientContext db;
        public HomeController(PatientContext context)
        {
            db = context;
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        //POST: api/Patient/Register
        public async Task<IActionResult> RegisterPatient(Models.ModelsView.RegisterModel model)
        {
            var patient = new Patient
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

            try
            {
                db.Patients.Add(patient);
                var result = await db.SaveChangesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Возвращает Email текущего пользователя
        /// </summary>
        /// <returns></returns>
        protected string CurrentEmail()
        {
            return User.Identity.Name;
        }

        /// <summary>
        /// Получение текущего пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUser")]
        public async Task<Patient> CurrentPatient()
        {
            var emailPatient = CurrentEmail();
            if (emailPatient == null)
                return null;

            var patient = await db.Patients.FirstOrDefaultAsync(p => p.email == emailPatient);
            return patient;
        }

        /// <summary>
        /// Получение токена доступа (Авторизация)
        /// </summary>
        /// <param name="username">Имя учетной записи (Email)</param>
        /// <param name="password">Пароль учетной записи</param>
        /// <returns>Токен доступа</returns>
        [HttpGet("Auth")]
        [AllowAnonymous]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return StatusCode(400, "Не указан логин и/или пароль");
            var token = AuthorizePatient(username, password);
            if (token == null)
                return BadRequest(new { errorText = "Не правильный логин или пароль." });

            return Ok(token);
        }


        /// <summary>
        /// Выдача токена по Email и паролю
        /// </summary>
        /// <param name="username">Email</param>
        /// <param name="password">Пароль</param>
        /// <returns>Токен</returns>
        private Token AuthorizePatient(string username, string password)
        {
            var identity = GetIdentity(username, password);
            if (identity == null)
                return null;


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

            return new Token(encodedJwt, identity.Name);
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
    }
}
