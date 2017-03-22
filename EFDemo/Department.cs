using System;
using System.Collections.Generic;

namespace DotNetDemos.EFDemo
{
    public class Department
    {
        public Department()
        {
            this.Students = new HashSet<Student>();
        }
            
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
