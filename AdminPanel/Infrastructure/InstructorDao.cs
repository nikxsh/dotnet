
using AdminPanel.DataAccess.MSSQL;
using AdminPanel.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace AdminPanel.DataAccess
{
    public class InstructorDao : IInstructorDao, IDisposable
    {
        private bool disposed = false;
        private SchoolDbContext dbContext = new SchoolDbContext();
        public void Create(Instructor instructor)
        {
            try
            {
                dbContext.Instructors.Add(instructor);
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
                Instructor instructor = GetDetails(id);

                instructor.OfficeAssignment = null;
                dbContext.Instructors.Remove(instructor);

                var department = dbContext.Departments
                    .Where(d => d.InstructorID == id)
                    .SingleOrDefault();
                if (department != null)
                {
                    department.InstructorID = null;
                }

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Instructor Edit(string[] selectedCourses, Instructor instructorToUpdate)
        {
            try
            {
                var selectedCoursesHS = new HashSet<string>(selectedCourses);
                var instructorCourses = new HashSet<int>
                    (instructorToUpdate.Courses.Select(c => c.CourseID));
                foreach (var course in dbContext.Courses)
                {
                    if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                    {
                        if (!instructorCourses.Contains(course.CourseID))
                        {
                            instructorToUpdate.Courses.Add(course);
                        }
                    }
                    else
                    {
                        if (instructorCourses.Contains(course.CourseID))
                        {
                            instructorToUpdate.Courses.Remove(course);
                        }
                    }
                }
                dbContext.SaveChanges();
                return dbContext.Instructors.Find(instructorToUpdate.ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Course> GetAssignedCourseData()
        {
            try
            {
                var courses = from s in dbContext.Courses
                              select s;
                return courses.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Course GetCourseData(int courseId)
        {
            try
            {
                var course = dbContext.Courses.Find(courseId);
                return course;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Instructor GetDetails(int? id)
        {
            try
            {
                Instructor instructor = dbContext.Instructors
                             .Include(i => i.OfficeAssignment)
                             .Where(i => i.ID == id)
                             .Single();
                return instructor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InstructorIndexData GetInstructors(int? id, int? courseID)
        {
            try
            {
                var viewModel = new InstructorIndexData();

                viewModel.Instructors = dbContext.Instructors
                    .Include(i => i.OfficeAssignment)
                    .Include(i => i.Courses.Select(c => c.Department))
                    .OrderBy(i => i.LastName);

                if (id != null)
                {
                    viewModel.Courses = viewModel.Instructors.Where(
                        i => i.ID == id.Value).Single().Courses;
                }

                if (courseID != null)
                {
                    var selectedCourse = viewModel.Courses.Where(x => x.CourseID == courseID).Single();
                    dbContext.Entry(selectedCourse).Collection(x => x.Enrollments).Load();
                    foreach (Enrollment enrollment in selectedCourse.Enrollments)
                    {
                        dbContext.Entry(enrollment).Reference(x => x.Student).Load();
                    }

                    viewModel.Enrollments = selectedCourse.Enrollments;
                }
                return viewModel;
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
