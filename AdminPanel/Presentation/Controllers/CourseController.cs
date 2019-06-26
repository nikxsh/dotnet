using System.Net;
using System.Web.Mvc;
using AdminPanel.Entity.Models;
using AdminPanel.DataAccess;
using System;

namespace AdminPanel.Presentation.Controllers
{
    public class CourseController : Controller
    {
        private CourseDao objCourseDao = new CourseDao();

        // GET: Course
        public ActionResult Index(int? SelectedDepartment)
        {
            try
            {
                var departments = objCourseDao.GetDepartments();
                ViewBag.SelectedDepartment = new SelectList(departments, "DepartmentID", "Name", SelectedDepartment);
                int departmentID = SelectedDepartment.GetValueOrDefault();
                var courses = objCourseDao.GetSelectedDepartment(SelectedDepartment, departmentID);
                return View(courses);
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                Course course = objCourseDao.GetDetails(id);
                if (course == null)
                {
                    return HttpNotFound();
                }
                return View(course);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


        public ActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Title,Credits,DepartmentID")]Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objCourseDao.Create(course);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            try
            {
                Course course = objCourseDao.GetDetails(id);
                if (course == null)
                {
                    return HttpNotFound();
                }
                PopulateDepartmentsDropDownList(course.DepartmentID);
                return View(course);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(Course course)
        {
            if (course == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            try
            {
                var courseToUpdate = objCourseDao.GetDetails(course.CourseID);
                if (TryUpdateModel(courseToUpdate, "",
                   new string[] { "Title", "Credits", "DepartmentID" }))
                {
                    PopulateDepartmentsDropDownList(courseToUpdate.DepartmentID);
                    objCourseDao.Edit(course);
                    return RedirectToAction("Index");
                }
                else
                    return View(courseToUpdate);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View();
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = objCourseDao.GetDepartments();
            ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment);
        }


        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            try
            {
                Course course = objCourseDao.GetDetails(id);
                if (course == null)
                {
                    return HttpNotFound();
                }
                return View(course);
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                objCourseDao.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult UpdateCourseCredits()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateCourseCredits(int multiplier)
        {
            ViewBag.RowsAffected = objCourseDao.UpdateCourseCredits(multiplier);
            return View();
        }
    }
}
