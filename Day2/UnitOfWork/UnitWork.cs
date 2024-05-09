using Day2.GenericRepos;
using Day2.Models;

namespace Day2.UnitOfWork
{
    public class UnitWork
    {
        private ITIContext db;
        private GenericRepo<Student> studentGenericRepo;
       private GenericRepo<Department> departmentGenericRepo;

        public GenericRepo<Student> StudentGenericRepo
        {
            get
            {
                if (studentGenericRepo == null)
                {
                    studentGenericRepo = new GenericRepo<Student>(db);
                }
                return studentGenericRepo;
            }
            
        }

        public GenericRepo<Department> DepartmentGenericRepo
        {
            get
            {
                if (departmentGenericRepo == null)
                {
                    departmentGenericRepo = new GenericRepo<Department>(db);
                }
                return departmentGenericRepo;
            }
        }


        public UnitWork(ITIContext dbContext)
        {
            db = dbContext;
        }



    }
}
