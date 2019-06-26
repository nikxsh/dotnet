using AdminPanel.Entity.Models;
using System.Collections.Generic;

namespace AdminPanel.DataAccess
{
    public interface ICourseDao
    {
        List<Course> GetSelectedDepartment(int? SelectedDepartment, int departmentID);
        Course GetDetails(int? id);
        List<Department> GetDepartments();
        void Create(Course course);
        int Edit(Course course);
        void Delete(int? id);
        int UpdateCourseCredits(int multiplier);
    }
}
