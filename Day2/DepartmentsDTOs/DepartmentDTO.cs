using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day2.DepartmentsDTOs
{
   
    public class DepartmentDTO
    {
        public int DeptId { get; set; }

        public string DeptName { get; set; }

        public string DeptDesc { get; set; }

        public string DeptLocation { get; set; }

        public int? DeptManager { get; set; }

        public int NumberOfStudents { get; set; }

    }
}
