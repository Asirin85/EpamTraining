
using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using RestApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("/student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            return _studentService.Get(id) switch
            {
                null => NotFound(),
                var student => student
            };
        }

        [HttpGet]
        public ActionResult<IReadOnlyCollection<Student>> GetStudents()
        {
            return _studentService.GetAll().ToArray();
        }

        [HttpPost]
        public IActionResult AddStudent(StudentValid student)
        {
            var newstudentId = _studentService.New(_mapper.Map<Student>(student));
            return Ok($"/student/{newstudentId}");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, StudentValid student)
        {
            var studentId = _studentService.Edit(_mapper.Map<Student>(student) with { Id = id });
            return Ok($"/student/{studentId}");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            _studentService.Delete(id);
            return Ok();
        }
    }
}
