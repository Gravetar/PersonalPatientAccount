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

        /// <summary>
        /// fsdd
        /// </summary>
        /// <returns></returns>
        //[HttpGet("GetCurrentUser")]
        //[DisableRequestSizeLimit]
        //[Produces("application/json")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(Exception), 400)]
        //public IActionResult GetCurrentUser()
        //{
        //    return db.Patients.ToList();
        //}
    }
}
