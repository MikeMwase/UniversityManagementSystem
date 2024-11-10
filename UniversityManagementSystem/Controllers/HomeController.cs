using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UniversityManagementSystem.Models;
using UniversityManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Models.UniversityViewModels;

namespace UniversityManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UniversityContext _context;

        public HomeController(ILogger<HomeController> logger, UniversityContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            IQueryable<EnrollmentDateGroup> data = from student in _context.Students
                                       group student by student.EnrolmentDate into dateGroup
                                       select new EnrollmentDateGroup()
                                       {
                                           EnrolmentDate = dateGroup.Key,
                                           StudentCount = dateGroup.Count()
                                       };

            return View(await data.AsNoTracking().ToListAsync());
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
