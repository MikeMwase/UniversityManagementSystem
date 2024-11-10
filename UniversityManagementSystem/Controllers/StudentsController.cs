using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using UniversityManagementSystem.Data;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UniversityContext _context;

        public StudentsController(UniversityContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? PageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortPara"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortPara"] = sortOrder == "Date" ? "date_desc" : "Date";

            if(searchString != null)
            {
                PageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var student = from s in _context.Students
                          select s;

            if(!String.IsNullOrEmpty(searchString))
            {
                student = student.Where(s => s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString));
            }

            switch(sortOrder)
            {
                case "name_desc": 
                    student = student.OrderByDescending(s => s.LastName);
                    break;

                case "Date":
                    student = student.OrderBy(s => s.EnrolmentDate);
                    break;

                case "date_desc":
                    student = student.OrderByDescending(s => s.EnrolmentDate);
                    break;

                default:
                    student = student.OrderBy(s => s.LastName);
                    break;
            }

            int PageSize = 2;

            return View(await PaginatedList<Student>.CreateAsync(student.AsNoTracking(),PageNumber ?? 1, PageSize));
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Enrolments)
                    .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName,FirstMidName,EnrolmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Unable to create user, try again and if the problem persists contact the system administrator");
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var studentToUpdate = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id);

            if(studentToUpdate == null)
            {
                return NotFound();
            }

            if(await TryUpdateModelAsync<Student>(studentToUpdate, "", s => s.LastName, s => s.FirstMidName, s => s.EnrolmentDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */) 
                {
                    ModelState.AddModelError(" ", "Unable to update user information. Please try again, if the problem persists contact the system administrator");
                }
            }

            return View(studentToUpdate);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            if(saveChangesError.GetValueOrDefault())
            {
                ViewData["Error Message"] = "Unable to delete the learner. Please try again, if the problem persists contact the system administrator";
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if(student == null)
            {
               RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(DbUpdateException /* ex */)
            {
                RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true});
            }

            return View(student);
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
