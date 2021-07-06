using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using PersonalPatientAccount.Models;
using PersonalPatientAccount.Models.ModelsView;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace PersonalPatientAccount.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<Patient> _userManager;
        private SignInManager<Patient> _signInManager;

        public AccountController(UserManager<Patient> userManager, SignInManager<Patient> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        

        private PatientContext _context;
        public AccountController(PatientContext context)
        {
            _context = context;
        }
        
    }
}
