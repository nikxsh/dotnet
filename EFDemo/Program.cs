using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetDemos.EFDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var choice = -1;
            Console.WriteLine("-------- Tree Structures --------");
            Console.WriteLine(" 1. Display Departments with Students");
            Console.WriteLine(" 10. Exit");
            Console.WriteLine("---------------------------------");

            CreateBasicDatabase();
            do
            {
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Display();
                        break;
                }
            } while (choice != 10);

            Console.ReadKey();
        }

        private static void CreateBasicDatabase()
        {
            using (var ctx = new SchoolContext())
            {
                Student stud1 = new Student() { StudentName = "Student 1", StudentId = new Guid("86080143-C088-4599-9CF5-0696550F66ED"), };
                Student stud2 = new Student() { StudentName = "Student 2", StudentId = new Guid("709B7ECC-E5DC-45A6-98A9-08279D156A1A"), };
                Student stud3 = new Student() { StudentName = "Student 3", StudentId = new Guid("19CF059F-0EF1-48B7-B84F-1C58A4D2AB03"), };
                Student stud4 = new Student() { StudentName = "Student 4", StudentId = new Guid("CBBB83BA-C2F7-4B2F-9014-20CA67877F2D"), };
                Student stud5 = new Student() { StudentName = "Student 5", StudentId = new Guid("1AE7D432-07A0-4D3A-A0C0-259C84C2F08A"), };
                Student stud6 = new Student() { StudentName = "Student 6", StudentId = new Guid("5CC07B54-B748-4CB3-A9C2-5791032DFA1C"), };

                Department dept1 = new Department() { DepartmentId = new Guid("F0158217-7148-4FDD-916B-643522D49313"), DepartmentName = "Science", Students = new List<Student>() };
                Department dept2 = new Department() { DepartmentId = new Guid("A4C82F2C-F090-4D08-83A8-5E2275BEB11E"), DepartmentName = "Art", Students = new List<Student>() };
                Department dept3 = new Department() { DepartmentId = new Guid("9D565F42-F3FD-4F6A-A1E8-0A5D054A2035"), DepartmentName = "Commerce", Students = new List<Student>() };

                dept1.Students.Add(stud1);
                dept1.Students.Add(stud2);
                dept1.Students.Add(stud5);

                dept2.Students.Add(stud3);
                dept2.Students.Add(stud4);

                dept3.Students.Add(stud6);

                if (!ctx.Departments.Any(x => x.DepartmentId == dept1.DepartmentId))
                    ctx.Entry(dept1).State = System.Data.Entity.EntityState.Added;
                if (!ctx.Departments.Any(x => x.DepartmentId == dept2.DepartmentId))
                    ctx.Entry(dept2).State = System.Data.Entity.EntityState.Added;
                if (!ctx.Departments.Any(x => x.DepartmentId == dept3.DepartmentId))
                    ctx.Entry(dept3).State = System.Data.Entity.EntityState.Added;

                ctx.SaveChanges();
            }
        }

        private static void Display()
        {
            using (var ctx = new SchoolContext())
            {               
                var recordList = ctx.Departments.ToList();

                foreach (var department in recordList)
                {
                    Console.WriteLine(" ---- {1} ----", department.DepartmentId, department.DepartmentName);
                    foreach (var student in department.Students)
                    {
                        Console.WriteLine("{1}", student.StudentId, student.StudentName);
                    }
                    Console.WriteLine("-----------------------------------------------------------------");
                }
            }
        }
    }

}
