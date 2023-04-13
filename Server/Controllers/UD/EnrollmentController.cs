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
        [Route("GetEnrollment")]
        public async Task<IActionResult> GetEnrollment()
        {
            return await _getHandler();
        }

        [HttpGet]
        [Route("GetEnrollment/{SchoolId}/{SectionId}/{StudentId}")]
        public async Task<IActionResult> GetEnrollmentByPK(int SchoolId, int SectionId, int StudentId)
        {
                return await _getByPkHandler(new EnrollmentPK
                {
                    StudentId = StudentId,
                    SchoolId = SchoolId,
                    SectionId = SectionId
                });
        }

        [HttpPost]
        [Route("PostEnrollment")]
        public async Task<IActionResult> PostEnrollment([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            return await _postHandler(_EnrollmentDTO);
        }

        [HttpPut]
        [Route("PutEnrollment")]
        public async Task<IActionResult> PutEnrollment([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            return await _putHandler(_EnrollmentDTO);
        }

        [HttpDelete]
        [Route("DeleteEnrollment/{SchoolId}/{SectionId}/{StudentId}")]
        public async Task<IActionResult> DeleteEnrollment(int SchoolId, int SectionId, int StudentId)
        {
                return await _deleteHandler(new EnrollmentPK
                {
                    StudentId = (int)StudentId,
                    SchoolId = (int)SchoolId,
                    SectionId = (int)SectionId
                });
        }
    }
}
