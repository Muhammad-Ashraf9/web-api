using System.ComponentModel;
using Day2.DepartmentsDTOs;
using Day2.Models;
using Day2.StudentDTOs;
using Day2.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Day2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly UnitWork unitWork;

        public DepartmentsController(UnitWork _unitWork)
        {
            unitWork = _unitWork;
        }


        /// <summary>
        /// get department by page size and page number
        /// </summary>
        /// <returns>
        /// pageNumber, pageSize, pagesNumber and departments list 
        /// </returns>
        /// <remarks>
        /// request example:
        /// GET /api/departments
        /// </remarks>

        [HttpGet]
        [SwaggerOperation(summary:"get all departments by page size and page number")]
        [SwaggerResponse(200,"list of department",typeof(List<DepartmentDTO>))]
        //[Authorize]
        public IActionResult GetAll(int pageNumber = 1, int pageSize = 10)
        {
            int totalCount = unitWork.DepartmentGenericRepo.GetAll().Count();

            int pagesNumber = (int)Math.Ceiling((double)totalCount / pageSize);

            int skipNo = (pageNumber - 1) * pageSize;

            List<Department> depts = unitWork.DepartmentGenericRepo.GetAll().Skip(skipNo).Take(pageSize).ToList();

            List<DepartmentDTO> departmentDTOsList = new List<DepartmentDTO>();

            foreach (Department dept in depts)
            {
                DepartmentDTO departmentDTO = new DepartmentDTO()
                {
                    DeptId = dept.DeptId,

                    DeptName = dept.DeptName,

                    DeptDesc = dept.DeptDesc,

                    DeptLocation = dept.DeptLocation,

                    DeptManager = dept.DeptManager,

                    NumberOfStudents = dept.Students?.Count() ?? 0

                };
                departmentDTOsList.Add(departmentDTO);
            }

            var query = new { pageNumber, pageSize, pagesNumber, departmentDTOsList };


            //return Ok(query);
            return Ok(departmentDTOsList);
        }

    }
}
