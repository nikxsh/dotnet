using System;
using System.Data;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using AdminPanel.Entity.Models;
using AdminPanel.DataAccess;

namespace AdminPanel.Presentation.Controllers
{
    public class DepartmentController : Controller
    {
        private DepartmentDao objDepartmentDao = new DepartmentDao();

        // GET: Department
        public ActionResult Index()
        {
            var departments = objDepartmentDao.GetDepartments();
            return View(departments.ToList());
        }

        // GET: Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                Department department = objDepartmentDao.GetDetails(id);

                if (department == null)
                {
                    return HttpNotFound();
                }
                return View(department);

            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            ViewBag.InstructorID = new SelectList(objDepartmentDao.GetInstructors(), "ID", "FullName");
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentID,Name,Budget,StartDate,InstructorID")] Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objDepartmentDao.Create(department);
                    return RedirectToAction("Index");
                }
                ViewBag.InstructorID = new SelectList(objDepartmentDao.GetInstructors(), "ID", "FullName", department.InstructorID);
                return View(department);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            try
            {
                Department department = objDepartmentDao.GetDetails(id);
                if (department == null)
                {
                    return HttpNotFound();
                }
                ViewBag.InstructorID = new SelectList(objDepartmentDao.GetInstructors(), "ID", "FullName", department.InstructorID);
                return View(department);

            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // POST: Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, byte[] rowVersion)
        {
            string[] fieldsToBind = new string[] { "Name", "Budget", "StartDate", "InstructorID", "RowVersion" };

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            try
            {
                var departmentToUpdate = objDepartmentDao.GetDetails(id);
                if (departmentToUpdate == null)
                {
                    Department deletedDepartment = new Department();
                    TryUpdateModel(deletedDepartment, fieldsToBind);
                    ModelState.AddModelError(string.Empty,
                        "Unable to save changes. The department was deleted by another user.");
                    ViewBag.InstructorID = new SelectList(objDepartmentDao.GetInstructors(), "ID", "FullName", deletedDepartment.InstructorID);
                    return View(deletedDepartment);
                }

                if (TryUpdateModel(departmentToUpdate, fieldsToBind))
                {
                    try
                    {
                        objDepartmentDao.Edit(departmentToUpdate, rowVersion);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex /* dex */)
                    {
                        //Log the error (uncomment dex variable name and add a line here to write a log.
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
                ViewBag.InstructorID = new SelectList(objDepartmentDao.GetInstructors(), "ID", "FullName", departmentToUpdate.InstructorID);
                return View(departmentToUpdate);
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }

        // GET: Department/Delete/5
        public ActionResult Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                Department department = objDepartmentDao.GetDetails(id);
                if (department == null)
                {
                    if (concurrencyError.GetValueOrDefault())
                    {
                        return RedirectToAction("Index");
                    }
                    return HttpNotFound();
                }
                return View(department);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // POST: Department/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Department department)
        {
            try
            {
                objDepartmentDao.Delete(department);
                return RedirectToAction("Index");
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again, and if the problem persists contact your system administrator.");
                return View(department);
            }
        }
    }
}
