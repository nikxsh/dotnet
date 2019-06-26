using System;
using System.Linq;
using System.Collections.Generic;
using AdminPanel.DataAccess.MSSQL;
using AdminPanel.Entity.Models;
using System.Data.Entity;

namespace AdminPanel.DataAccess.Infrastructure
{
    public class StudentsDao : IStudentsDao, IDisposable
    {
        private bool disposed = false;
        private SchoolDbContext dbContext = new SchoolDbContext();

        public void Create(Student student)
        {
            try
            {
                dbContext.Students.Add(student);
                dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int? id)
        {
            try
            {
                Student student = dbContext.Students.Find(id);
                dbContext.Students.Remove(student);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Edit(Student student)
        {
            try
            {
                var stud = dbContext.Students.Find(student.ID);
                stud.FirstMidName = student.FirstMidName;
                stud.LastName = student.LastName;
                stud.EnrollmentDate = student.EnrollmentDate;
                dbContext.Entry(stud).State = EntityState.Modified;
                int result = dbContext.SaveChanges();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Student GetDetails(int? id)
        {
            try
            {
                return dbContext.Students.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EnrollmentDateGroup> GetEnrollmentDateGroup()
        {
            try
            {
                string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
                                 + "FROM Person "
                                 + "WHERE Discriminator = 'Student' "
                                 + "GROUP BY EnrollmentDate";
                return dbContext.Database.SqlQuery<EnrollmentDateGroup>(query).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Student> GetStudents(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                var students = from s in dbContext.Students
                               select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    students = students.Where(s => s.LastName.Contains(searchString)
                                           || s.FirstMidName.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        students = students.OrderByDescending(s => s.LastName);
                        break;
                    case "Date":
                        students = students.OrderBy(s => s.EnrollmentDate);
                        break;
                    case "date_desc":
                        students = students.OrderByDescending(s => s.EnrollmentDate);
                        break;
                    default:  // Name ascending 
                        students = students.OrderBy(s => s.LastName);
                        break;
                }

                return students.ToList();

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
