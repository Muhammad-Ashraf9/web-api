using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day2.StudentDTOs
{
    public class StudentDTO 
    {
      
        public int StId { get; set; }

        public string StFname { get; set; }

        public string StLname { get; set; }

        public string StAddress { get; set; }

        public int? StAge { get; set; }

        public string DeptName { get; set; }

        public int? DeptId { get; set; }

        public string StSuper { get; set; }

    }
}
