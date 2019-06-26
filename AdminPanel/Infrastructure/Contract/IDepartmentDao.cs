using AdminPanel.Entity.Models;
using System.Collections.Generic;

namespace AdminPanel.DataAccess
{
    public interface IDepartmentDao
    {
        List<Department> GetDepartments();
        Department GetDetails(int? id);
        void Create(Department department);
        Department Edit(Department departmentToUpdate, byte[] rowVersion);
        void Delete(Department department);
        List<Instructor> GetInstructors();
    }
}
