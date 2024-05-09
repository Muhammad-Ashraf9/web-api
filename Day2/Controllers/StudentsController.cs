using Day2.Models;
using Day2.StudentDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ITIContext db;

        public StudentsController(ITIContext _dbContext)
        {
            db = _dbContext;
        }

        [HttpGet]


        /// <summary>
        /// get student by page size and page number
        /// </summary>
        /// <returns>
        /// pageNumber, pageSize, pagesNumber and students list 
        /// </returns>
        /// <remarks>
        /// request example:
        /// GET /api/students
        /// </remarks>

        //[HttpGet("/api/students/all")]

        public IActionResult Get(int pageNumber = 1, int pageSize = 10)
        {
            int totalCount = db.Students.Count();

            int pagesNumber = (int)Math.Ceiling((double)totalCount / pageSize);

            int skipNo = (pageNumber - 1) * pageSize;

            List<Student> students = db.Students.Skip(skipNo).Take(pageSize).ToList();

            List<StudentDTO> studentDTOsList = new List<StudentDTO>();

            foreach (Student student in students)
            {
                StudentDTO studentDTO = new StudentDTO()
                {
                    StId = student.StId,
                    StAddress = student.StAddress,
                    StAge = student.StAge,
                    StFname = student.StFname,
                    StLname = student.StLname,
                    StSuper = $"{student.StSuperNavigation?.StFname ?? ""} {student.StSuperNavigation?.StLname ?? ""}",
                    DeptName = student.Dept?.DeptName ?? "no department found",
                    DeptId = student.DeptId 
                };
                studentDTOsList.Add(studentDTO);
            }

            var query = new { pageNumber, pageSize, pagesNumber, studentDTOsList };


            //return Ok(query);
            return Ok(studentDTOsList);
        }

        /// <summary>
        /// get student by id
        /// </summary>
        /// <param name="id">id of the student</param>
        /// <returns>student object</returns>
        /// <remarks>
        /// request example:
        /// GET /api/students/1
        /// </remarks>

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Student? student = db.Students.Find(id);

            if (student == null)
            {
                return NotFound();
            }

            StudentDTO studentDto = new StudentDTO()
            {
                StId = student.StId,
                StAddress = student.StAddress,
                StAge = student.StAge,
                StFname = student.StFname,
                StLname = student.StLname,
                StSuper = $"{student.StSuperNavigation?.StFname ?? ""} {student.StSuperNavigation?.StLname ?? ""}",
                DeptName = student.Dept?.DeptName ?? "no department found",
                DeptId = student.DeptId

            };
            return Ok(studentDto);
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult Add(PostStudentDTO student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            Student s = new Student()
            {
                StId = student.Id,
                StAddress = student.Address,
                StAge = student.Age,
                StFname = student.FName,
                StLname = student.LName,
            };
            db.Students.Add(s);
            db.SaveChanges();
            return CreatedAtAction("Add", new { id = s.StId }, s);
        }

        [HttpGet("/api/stdname/{name}")]
        public IActionResult GetByName(string name)
        {
            var students = db.Students.Where(s => s.StFname.Contains(name) || s.StLname.Contains(name)).ToList();

            List<StudentDTO> studentsDtoList = new List<StudentDTO>();

            if (students == null)
            {
                return NotFound();
            }

            foreach (Student student in students)
            {
                StudentDTO studentDto = new StudentDTO()
                {
                    StFname = student.StFname,
                    StAddress = student.StAddress,
                    StAge = student.StAge,
                    DeptName = student.Dept?.DeptName ?? "",
                    StLname = student.StLname,
                    StSuper = student.StSuperNavigation?.StFname ?? "",
                    DeptId = student.DeptId

                };
                studentsDtoList.Add(studentDto);
            }

            return Ok(studentsDtoList);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, EditStudentBodyDTO body)
        {
            //if (!ModelState.IsValid)
            //{
            //    return ValidationProblem();
            //}
            Student foundStd = db.Students.Find(id);

            if (foundStd == null)
            {
                return NotFound();
            }

            foundStd.StFname = body.StFname;
            foundStd.StLname = body.StLname;
            foundStd.StAddress = body.StAddress;
            foundStd.StAge = body.StAge;
            foundStd.DeptId = body.DeptId;


            db.Students.Update(foundStd);

            db.SaveChanges();

            body.StId = foundStd.StId;

            return Ok(body);
        }
    }
}
