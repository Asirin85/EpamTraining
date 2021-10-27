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
    [Route("/attendance")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IMapper _mapper;
        public AttendanceController(IAttendanceService attendanceService, IMapper mapper)
        {
            _attendanceService = attendanceService;
            _mapper = mapper;
        }
        [HttpGet("{studentId}/{lectureId}")]
        public ActionResult<Attendance> GetAttendance(int studentId, int lectureId)
        {
            return _attendanceService.Get(studentId, lectureId) switch
            {
                null => NotFound(),
                var attendance => attendance
            };
        }
        [HttpGet]
        public ActionResult<IReadOnlyCollection<Attendance>> GetAttendances()
        {
            return _attendanceService.GetAll().ToArray();
        }
        [HttpPost]
        public IActionResult AddAttendance(AttendanceValid attendance)
        {
            var newAttendanceId = _attendanceService.New(_mapper.Map<Attendance>(attendance));
            return Ok($"/attendance/{newAttendanceId.studentId}/{newAttendanceId.lectureId}");
        }
        [HttpPut("{studentId}/{lectureId}")]
        public IActionResult UpdateAttendance(int studentId, int lectureId, AttendanceValid attendance)
        {
            var attendanceId = _attendanceService.Edit(_mapper.Map<Attendance>(attendance) with { StudentId = studentId, LectureId = lectureId });
            return Ok($"/attendance/{attendanceId.studentId}/{attendanceId.lectureId}");
        }
        [HttpDelete("{studentId}/{lectureId}")]
        public IActionResult DeleteAttendance(int studentId, int lectureId)
        {
            _attendanceService.Delete(studentId, lectureId);
            return Ok();
        }
    }
}
