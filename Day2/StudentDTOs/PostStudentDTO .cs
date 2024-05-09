using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day2.StudentDTOs
{
    public class PostStudentDTO
    {
      
        public int Id { get; set; }
        public string FName { get; set; }

        public string LName { get; set; }

        public string Address { get; set; }

        public int? Age { get; set; }


    }
}
