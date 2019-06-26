using System;
using System.Net;
using System.Web.Mvc;
using AdminPanel.Entity.Models;
using AdminPanel.DataAccess.Infrastructure;
using PagedList;

namespace AdminPanel.Presentation.Controllers
{
    public class StudentController : Controller
    {
        private StudentsDao objStudents = new StudentsDao();

        // GET: Student
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var students = objStudents.GetStudents(sortOrder, currentFilter, searchString, page);
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(students.ToPagedList(pageNumber, pageSize));

            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }


        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                Student student = objStudents.GetDetails(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);

            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName, FirstMidName, EnrollmentDate")]Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objStudents.Create(student);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(student);
        }


        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Student student = objStudents.GetDetails(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost([Bind(Include = "ID,LastName, FirstMidName, EnrollmentDate")]Student student)
        {
            try
            {
                if (student == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var studentToUpdate = objStudents.GetDetails(student.ID);
                try
                {
                    int result = objStudents.Edit(student);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return View(studentToUpdate);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                if (saveChangesError.GetValueOrDefault())
                {
                    ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
                }
                Student student = objStudents.GetDetails(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                objStudents.Delete(id);
            }
            catch (Exception ex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}
