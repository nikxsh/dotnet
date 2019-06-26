using AdminPanel.Entity.Models;
using System.Collections.Generic;

namespace AdminPanel.DataAccess.Infrastructure
{
    public interface IStudentsDao
    {
        List<Student> GetStudents(string sortOrder, string currentFilter, string searchString, int? page);
        Student GetDetails(int? id);
        void Create(Student student);
        int Edit(Student student);
        void Delete(int? id);
        List<EnrollmentDateGroup> GetEnrollmentDateGroup();

    }
}
