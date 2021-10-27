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
    [Route("/lecturer")]
    public class LecturerController : ControllerBase
    {
        private readonly ILecturerService _lecturerService;
        private readonly IMapper _mapper;

        public LecturerController(ILecturerService lecturerService, IMapper mapper)
        {
            _lecturerService = lecturerService;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public ActionResult<Lecturer> GetLecturer(int id)
        {
            return _lecturerService.Get(id) switch
            {
                null => NotFound(),
                var lecturer => lecturer
            };
        }

        [HttpGet]
        public ActionResult<IReadOnlyCollection<Lecturer>> GetLecturers()
        {
            return _lecturerService.GetAll().ToArray();
        }

        [HttpPost]
        public IActionResult AddLecturer(LecturerValid lecturer)
        {
            var newlecturerId = _lecturerService.New(_mapper.Map<Lecturer>(lecturer));
            return Ok($"/lecturer/{newlecturerId}");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLecturer(int id, LecturerValid lecturer)
        {
            var lecturerId = _lecturerService.Edit(_mapper.Map<Lecturer>(lecturer) with { Id = id });
            return Ok($"/lecturer/{lecturerId}");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLecturer(int id)
        {
            _lecturerService.Delete(id);
            return Ok();
        }
    }
}
