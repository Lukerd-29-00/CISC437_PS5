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
    public class EnrollmentController : BaseController<Enrollment, EnrollmentDTO, EnrollmentPK>
    {

        public EnrollmentController(DOOROracleContext _DBcontext,
            IOraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        protected override DbSet<Enrollment> _getContext()
        {
            return _context.Enrollments;
        }


        protected override Expression<Func<Enrollment, EnrollmentDTO>> _getDTOExp()
        {
            return sp => new EnrollmentDTO
            {
                StudentId = sp.StudentId,
                SchoolId = sp.SchoolId,
                SectionId = sp.SectionId,
                EnrollDate = sp.EnrollDate,
                FinalGrade = sp.FinalGrade,
                CreatedBy = sp.CreatedBy,
                CreatedDate = sp.CreatedDate,
                ModifiedBy = sp.ModifiedBy,
                ModifiedDate = sp.ModifiedDate
            };
        }

        protected override void _mutateRecord(Enrollment enr, EnrollmentDTO dto)
        {
            enr.EnrollDate = dto.EnrollDate;
            enr.FinalGrade = dto.FinalGrade;

        }

        protected override Expression<Func<Enrollment, bool>> _getPredicate(EnrollmentPK Pkey)
        {
            return sp => (sp.StudentId == Pkey.StudentId) && (sp.SchoolId == Pkey.SchoolId) && (sp.SectionId == Pkey.SectionId);
        }
        [HttpGet]
        public async Task<IActionResult> GetEnrollmentByPK([FromQuery] int? StudentId, [FromQuery] int? SchoolId, int? SectionId)
        {
            if (StudentId == null && SchoolId == null && SectionId == null)
                return await _getHandler();
            else if (StudentId == null || SchoolId == null || SectionId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Please send a complete primary key");
            else
                return await _getByPkHandler(new EnrollmentPK
                {
                    StudentId = (int)StudentId,
                    SchoolId = (int)SchoolId,
                    SectionId = (int)SectionId
                });
        }

        [HttpPost]
        public async Task<IActionResult> PostEnrollment([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            return await _postHandler(_EnrollmentDTO);
        }

        [HttpPut]
        public async Task<IActionResult> PutEnrollment([FromBody] EnrollmentDTO _EnrollmentDTO, [FromQuery] int? EnrollmentNo, [FromQuery] int? SchoolId)
        {
            if (EnrollmentNo != null && SchoolId != null)
                return await _putHandler(_EnrollmentDTO);
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Please provide a complete primary key through the query string.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEnrollment([FromQuery] int? StudentId, [FromQuery] int? SchoolId, int? SectionId)
        {
            if (StudentId != null && SchoolId != null && SectionId != null)
                return await _deleteHandler(new EnrollmentPK
                {
                    StudentId = (int)StudentId,
                    SchoolId = (int)SchoolId,
                    SectionId = (int)SectionId
                });
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Please provide a complete primary key.");
        }
    }
}
