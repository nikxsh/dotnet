using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AdminPanel.ViewModels;
using AdminPanel.DataAccess.Infrastructure;
using System;

namespace AdminPanel.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private StudentsDao objStudents = new StudentsDao();
        public ActionResult Index()
        {
            try
            {
                var data = objStudents.GetEnrollmentDateGroup();
                List<EnrollmentDateGroup> op = new List<EnrollmentDateGroup>();
                op.Add(new EnrollmentDateGroup { EnrollmentDate = data.FirstOrDefault().EnrollmentDate, StudentCount = data.FirstOrDefault().StudentCount });
                return View(op);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}