using System;
using System.Collections.Generic;
using System.Linq;
using AdminPanel.Entity.Models;
using AdminPanel.DataAccess.MSSQL;
using System.Data.Entity;

namespace AdminPanel.DataAccess
{
    public class CourseDao : ICourseDao, IDisposable
    {
        private bool disposed = false;
        private SchoolDbContext dbContext = new SchoolDbContext();
        public void Create(Course course)
        {
            try
            {
                dbContext.Courses.Add(course);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int? id)
        {
            try
            {
                Course course = dbContext.Courses.Find(id);
                dbContext.Courses.Remove(course);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Edit(Course course)
        {
            try
            {
                var courseData = dbContext.Courses.Find(course.CourseID);
                courseData.DepartmentID = course.DepartmentID;
                courseData.Credits = course.Credits;
                courseData.Title = course.Title;
                dbContext.Entry(courseData).State = EntityState.Modified;
                return dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Department> GetDepartments()
        {
            try
            {
                return dbContext.Departments.OrderBy(q => q.Name).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Course GetDetails(int? id)
        {
            try
            {
                return dbContext.Courses.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Course> GetSelectedDepartment(int? SelectedDepartment, int departmentID)
        {
            try
            {
                IQueryable<Course> courses = dbContext.Courses
                 .Where(c => !SelectedDepartment.HasValue || c.DepartmentID == departmentID)
                 .OrderBy(d => d.CourseID)
                 .Include(d => d.Department);
                var sql = courses.ToString();
                return courses.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateCourseCredits(int multiplier)
        {
            try
            {
                return dbContext.Database.ExecuteSqlCommand("UPDATE Course SET Credits = Credits * {0}", multiplier);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            // Dispose of managed resources here.
            if (disposing)
                dbContext.Dispose();

            // Dispose of any unmanaged resources not wrapped in dbContext.
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
