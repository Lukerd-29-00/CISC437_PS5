using DOOR.EF.Data;
using DOOR.EF.Models;
using DOOR.Server.Controllers.Common;
using DOOR.Shared.DTO;
using DOOR.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : BaseController<Course, CourseDTO, CoursePK>
    {

        public CourseController(DOOROracleContext _DBcontext,
            IOraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        protected override DbSet<Course> _getContext()
        {
            return _context.Courses;
        }


        protected override Expression<Func<Course, CourseDTO>> _getDTOExp()
        {
            return sp => new CourseDTO
            {
                CourseNo = sp.CourseNo,
                Description = sp.Description,
                Cost = sp.Cost,
                Prerequisite = sp.Prerequisite,
                SchoolId = sp.SchoolId,
                PrerequisiteSchoolId = sp.PrerequisiteSchoolId,
                CreatedBy = sp.CreatedBy,
                CreatedDate = sp.CreatedDate,
                ModifiedDate = sp.ModifiedDate,
                ModifiedBy = sp.ModifiedBy
            };
        }

        protected override void _mutateRecord(Course course, CourseDTO dto)
        {
            course.Description = dto.Description;
            course.Cost = dto.Cost;
            course.Prerequisite = dto.Prerequisite;
            course.SchoolId = dto.SchoolId;
            course.PrerequisiteSchoolId = dto.PrerequisiteSchoolId;
        }

        protected override Expression<Func<Course, bool>> _getPredicate(CoursePK Pkey)
        {
            return sp => (sp.CourseNo == Pkey.CourseNo) && (sp.SchoolId == Pkey.SchoolId);
        }

        [HttpGet]
        [Route("GetCourse")]
        public async Task<IActionResult> GetCourse()
        {
            return await _getHandler();
        }

        [HttpGet]
        [Route("GetCourse/{SchoolId}/{CourseNo}")]
        public async Task<IActionResult> GetCourseByPK(int SchoolId, int CourseNo)
        {
            return await _getByPkHandler(new CoursePK { CourseNo = CourseNo, SchoolId = SchoolId });
        }

        [HttpPost]
        [Route("PostCourse")]
        public async Task<IActionResult> PostCourse([FromBody] CourseDTO _CourseDTO)
        {
            return await _postHandler(_CourseDTO);
        }

        [HttpPut]
        [Route("PutCourse")]
        public async Task<IActionResult> PutCourse([FromBody] CourseDTO _CourseDTO)
        {
            return await _putHandler(_CourseDTO);
        }

        [HttpDelete]
        [Route("DeleteCourse/{SchoolId}/{CourseNo}/")]
        public async Task<IActionResult> DeleteCourse(int SchoolId, int CourseNo)
        {
            return await _deleteHandler(new CoursePK { CourseNo = CourseNo, SchoolId = SchoolId });

        }
    }
}