using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Data;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Views
{
    public class FacultyAssignmentsController : Controller
    {
        private readonly VarsityContext _context;

        public FacultyAssignmentsController(VarsityContext context)
        {
            _context = context;
        }

        // GET: FacultyAssignments
        public async Task<IActionResult> Index()
        {
            var varsityContext = _context.OfficeAssignments.Include(f => f.Lecturer);
            return View(await varsityContext.ToListAsync());
        }

        // GET: FacultyAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyAssignment = await _context.OfficeAssignments
                .Include(f => f.Lecturer)
                .FirstOrDefaultAsync(m => m.LecturerID == id);
            if (facultyAssignment == null)
            {
                return NotFound();
            }

            return View(facultyAssignment);
        }

        // GET: FacultyAssignments/Create
        public IActionResult Create()
        {
            ViewData["LecturerID"] = new SelectList(_context.lectures, "ID", "FirstMidName");
            return View();
        }

        // POST: FacultyAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LecturerID,Location")] FacultyAssignment facultyAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facultyAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LecturerID"] = new SelectList(_context.lectures, "ID", "FirstMidName", facultyAssignment.LecturerID);
            return View(facultyAssignment);
        }

        // GET: FacultyAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyAssignment = await _context.OfficeAssignments.FindAsync(id);
            if (facultyAssignment == null)
            {
                return NotFound();
            }
            ViewData["LecturerID"] = new SelectList(_context.lectures, "ID", "FirstMidName", facultyAssignment.LecturerID);
            return View(facultyAssignment);
        }

        // POST: FacultyAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LecturerID,Location")] FacultyAssignment facultyAssignment)
        {
            if (id != facultyAssignment.LecturerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facultyAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyAssignmentExists(facultyAssignment.LecturerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LecturerID"] = new SelectList(_context.lectures, "ID", "FirstMidName", facultyAssignment.LecturerID);
            return View(facultyAssignment);
        }

        // GET: FacultyAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyAssignment = await _context.OfficeAssignments
                .Include(f => f.Lecturer)
                .FirstOrDefaultAsync(m => m.LecturerID == id);
            if (facultyAssignment == null)
            {
                return NotFound();
            }

            return View(facultyAssignment);
        }

        // POST: FacultyAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facultyAssignment = await _context.OfficeAssignments.FindAsync(id);
            _context.OfficeAssignments.Remove(facultyAssignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultyAssignmentExists(int id)
        {
            return _context.OfficeAssignments.Any(e => e.LecturerID == id);
        }
    }
}
