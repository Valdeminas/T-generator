using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using T_generator.Data;
using Microsoft.AspNetCore.Identity;
using T_generator.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace T_generator.Controllers
    {
    public class HomeController : Controller
        {
        private const string MESSAGE = "Message";

        public IActionResult Index()
            {
            return View();
            }

        public IActionResult About()
            {
            ViewData[MESSAGE] = "Your application description page.";

            return View();
            }

        public IActionResult Contact()
            {
            ViewData[MESSAGE] = "Your contact page.";

            return View();
            }

        public IActionResult Error()
            {
            return View();
            }
        }
    }
