using AdminPanel.DataAccess.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using AdminPanel.Entity.Models;
using System.Data.Entity;

namespace AdminPanel.DataAccess
{
    public class DepartmentDao : IDepartmentDao,IDisposable
    {
        private bool disposed = false;
        private SchoolDbContext dbContext = new SchoolDbContext();

        public void Create(Department department)
        {
            try
            {
                dbContext.Departments.Add(department);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Department department)
        {
            try
            {
                dbContext.Entry(department).State = EntityState.Deleted;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Department Edit(Department departmentToUpdate, byte[] rowVersion)
        {
            try
            {
                dbContext.Entry(departmentToUpdate).OriginalValues["RowVersion"] = rowVersion;
                dbContext.SaveChanges();
                return departmentToUpdate;
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
                var departments = dbContext.Departments.Include(d => d.Administrator);
                return departments.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Department GetDetails(int? id)
        {
            try
            {
                // Create and execute raw SQL query.
                string query = "SELECT * FROM Department WHERE DepartmentID = @p0";
                Department department = dbContext.Departments.SqlQuery(query, id).SingleOrDefault();
                return department;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Instructor> GetInstructors()
        {
            try
            {
                var instructors = dbContext.Instructors;
                return instructors.ToList();
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
