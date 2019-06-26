using AdminPanel.Entity.Models;
using System.Collections.Generic;

namespace AdminPanel.DataAccess
{
    public interface IInstructorDao
    {
        InstructorIndexData GetInstructors(int? id, int? courseID);
        List<Course> GetAssignedCourseData();
        Instructor GetDetails(int? id);
        Course GetCourseData(int courseId);
        void Create(Instructor instructor);
        Instructor Edit(string[] selectedCourses, Instructor instructorToUpdate);
        void Delete(int? id);
    }
}
