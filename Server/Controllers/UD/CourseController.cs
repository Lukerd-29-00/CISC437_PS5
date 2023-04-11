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

        protected override Expression<Func<Course, bool>> _getPredicate(CoursePK Pkey)
        {
            return sp => (sp.CourseNo == Pkey.CourseNo) && (sp.SchoolId == Pkey.SchoolId);
        }
        [HttpGet]
        public async Task<IActionResult> GetCourseByPK([FromQuery] int? CourseNo, [FromQuery] int? SchoolId)
        {
            if (CourseNo == null && SchoolId == null)
                return await _getHandler();
            else if (CourseNo == null || SchoolId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Please send a complete primary key");
            else
                return await _getByPkHandler(new CoursePK { CourseNo = (int)CourseNo, SchoolId = (int)SchoolId });
        }

        [HttpPost]
        public async Task<IActionResult> PostCourse([FromBody] CourseDTO _CourseDTO)
        {
            return await _postHandler(_CourseDTO);
        }

        [HttpPut]
        public async Task<IActionResult> PutCourse([FromBody] CourseDTO _CourseDTO)
        {
            return await _putHandler(_CourseDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourse([FromQuery] int? CourseNo, [FromQuery] int? SchoolId)
        {
            if (CourseNo != null && SchoolId != null)
                return await _deleteHandler(new CoursePK { CourseNo = (int)CourseNo, SchoolId = (int)SchoolId });
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Please provide a complete primary key.");
        }
    }
}