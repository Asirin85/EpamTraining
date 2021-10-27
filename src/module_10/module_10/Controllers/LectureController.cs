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
    [Route("/lecture")]
    public class LectureController : ControllerBase
    {
        private readonly ILectureService _lectureService;
        private readonly IMapper _mapper;

        public LectureController(ILectureService lectureService, IMapper mapper)
        {
            _lectureService = lectureService;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public ActionResult<Lecture> GetLecture(int id)
        {
            return _lectureService.Get(id) switch
            {
                null => NotFound(),
                var lecture => lecture
            };
        }

        [HttpGet]
        public ActionResult<IReadOnlyCollection<Lecture>> GetLectures()
        {
            return _lectureService.GetAll().ToArray();
        }

        [HttpPost]
        public IActionResult AddLecture(LectureValid lecture)
        {
            var newlectureId = _lectureService.New(_mapper.Map<Lecture>(lecture));
            return Ok($"/lecture/{newlectureId}");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLecture(int id, LectureValid lecture)
        {
            var lectureId = _lectureService.Edit(_mapper.Map<Lecture>(lecture) with { Id = id });
            return Ok($"/lecture/{lectureId}");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLecture(int id)
        {
            _lectureService.Delete(id);
            return Ok();
        }
    }
}
