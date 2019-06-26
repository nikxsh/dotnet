using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AdminPanel.Entity.Models;
using AdminPanel.ViewModels;
using AdminPanel.DataAccess;

namespace AdminPanel.Presentation.Controllers
{
    public class InstructorController : Controller
    {
        private InstructorDao objInstructorDao = new InstructorDao();

        // GET: Instructor
        public ActionResult Index(int? id, int? courseID)
        {
            try
            {
                var data = objInstructorDao.GetInstructors(id, courseID);
                ViewModels.InstructorIndexData viewModel = new ViewModels.InstructorIndexData();
                viewModel.Courses = data.Courses;
                viewModel.Enrollments = data.Enrollments;
                viewModel.Instructors = data.Instructors;
                if (id != null)
                    ViewBag.InstructorID = id.Value;
                if (courseID != null)
                    ViewBag.CourseID = courseID.Value;
                return View(viewModel);

            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


        // GET: Instructor/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Instructor instructor = objInstructorDao.GetDetails(id);
                if (instructor == null)
                {
                    return HttpNotFound();
                }
                return View(instructor);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            try
            {
                var instructor = new Instructor();
                instructor.Courses = new List<Course>();
                PopulateAssignedCourseData(instructor);
                return View();
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstMidName,HireDate,OfficeAssignment")]Instructor instructor, string[] selectedCourses)
        {
            try
            {
                if (selectedCourses != null)
                {
                    instructor.Courses = new List<Course>();
                    foreach (var course in selectedCourses)
                    {
                        var courseToAdd = objInstructorDao.GetCourseData(int.Parse(course));
                        instructor.Courses.Add(courseToAdd);
                    }
                }
                if (ModelState.IsValid)
                {
                    objInstructorDao.Create(instructor);
                    return RedirectToAction("Index");
                }
                PopulateAssignedCourseData(instructor);
                return View(instructor);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


        // GET: Instructor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                Instructor instructor = objInstructorDao.GetDetails(id);
                PopulateAssignedCourseData(instructor);
                if (instructor == null)
                {
                    return HttpNotFound();
                }
                return View(instructor);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        private void PopulateAssignedCourseData(Instructor instructor)
        {
            try
            {
                var allCourses = objInstructorDao.GetAssignedCourseData();
                var instructorCourses = new HashSet<int>(instructor.Courses.Select(c => c.CourseID));
                var viewModel = new List<AssignedCourseData>();
                foreach (var course in allCourses)
                {
                    viewModel.Add(new AssignedCourseData
                    {
                        CourseID = course.CourseID,
                        Title = course.Title,
                        Assigned = instructorCourses.Contains(course.CourseID)
                    });
                }
                ViewBag.Courses = viewModel;

            }
            catch (Exception ex)
            {
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCourses)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var instructorToUpdate = objInstructorDao.GetDetails(id);
            try
            {
                if (TryUpdateModel(instructorToUpdate, "",
                   new string[] { "LastName", "FirstMidName", "HireDate", "OfficeAssignment" }))
                {
                    if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment.Location))
                    {
                        instructorToUpdate.OfficeAssignment = null;
                    }

                    objInstructorDao.Edit(selectedCourses, instructorToUpdate);

                    return RedirectToAction("Index");
                }
                PopulateAssignedCourseData(instructorToUpdate);
            }
            catch (Exception ex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(instructorToUpdate);
        }

        // GET: Instructor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                Instructor instructor = objInstructorDao.GetDetails(id);
                if (instructor == null)
                {
                    return HttpNotFound();
                }
                return View(instructor);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // POST: Instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            objInstructorDao.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
