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
    [Route("/homework")]
    public class HomeworkController : ControllerBase
    {
        private readonly IHomeworkService _homeworkService;
        private readonly IMapper _mapper;

        public HomeworkController(IHomeworkService homeworkService, IMapper mapper)
        {
            _homeworkService = homeworkService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<Homework> GetHomework(int id)
        {
            return _homeworkService.Get(id) switch
            {
                null => NotFound(),
                var homework => homework
            };
        }
        [HttpGet]
        public ActionResult<IReadOnlyCollection<Homework>> GetHomeworks()
        {
            return _homeworkService.GetAll().ToArray();
        }
        [HttpPost]
        public IActionResult AddHomework(HomeworkValid homework)
        {
            var newHomeworkId = _homeworkService.New(_mapper.Map<Homework>(homework));
            return Ok($"/homework/{newHomeworkId}");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateHomework(int id, HomeworkValid homework)
        {
            var homeworkId = _homeworkService.Edit(_mapper.Map<Homework>(homework) with { Id = id });
            return Ok($"/homework/{homeworkId}");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteHomework(int id)
        {
            _homeworkService.Delete(id);
            return Ok();
        }
    }
}
