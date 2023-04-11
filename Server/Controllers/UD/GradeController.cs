using DOOR.EF.Data;
using DOOR.EF.Models;
using DOOR.Server.Controllers.Common;
using DOOR.Shared.DTO;
using DOOR.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static System.Collections.Specialized.BitVector32;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : BaseController<Grade, GradeDTO, GradePK>
    {

        public GradeController(DOOROracleContext _DBcontext,
            IOraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        protected override DbSet<Grade> _getContext()
        {
            return _context.Grades;
        }


        protected override Expression<Func<Grade, GradeDTO>> _getDTOExp()
        {
            return sp => new GradeDTO
            {
                SchoolId = sp.SchoolId,
                StudentId = sp.StudentId,
                SectionId = sp.SectionId,
                GradeTypeCode = sp.GradeTypeCode,
                GradeCodeOccurrence = sp.GradeCodeOccurrence,
                NumericGrade = sp.NumericGrade,
                Comments = sp.Comments,
                CreatedBy = sp.CreatedBy,
                CreatedDate = sp.CreatedDate,
                ModifiedBy = sp.ModifiedBy,
                ModifiedDate = sp.ModifiedDate
            };
        }

        protected override Expression<Func<Grade, bool>> _getPredicate(GradePK Pkey)
        {
            return sp => (sp.SchoolId == Pkey.SchoolId) && (sp.StudentId== Pkey.StudentId) && (Pkey.SectionId == sp.SectionId) && (Pkey.GradeTypeCode == sp.GradeTypeCode) && (Pkey.GradeCodeOccurance == sp.GradeCodeOccurrence);
        }
        [HttpGet]
        public async Task<IActionResult> GetGradeByPK([FromQuery] int? SchoolId, [FromQuery] int? StudentId, [FromQuery] int? SectionId, [FromQuery] string? GradeTypeCode, byte? GradeCodeOccurance)
        {
            if (SchoolId == null && StudentId == null && SectionId == null && GradeTypeCode == null && GradeCodeOccurance == null)
                return await _getHandler();
            else if (SchoolId == null || StudentId == null || SectionId == null || GradeTypeCode == null || GradeCodeOccurance == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Please send a complete primary key");
            else
                return await _getByPkHandler(new GradePK
                {
                    SchoolId = (int)SchoolId,
                    StudentId = (int)StudentId,
                    SectionId = (int)SectionId,
                    GradeTypeCode = GradeTypeCode,
                    GradeCodeOccurance = (byte)GradeCodeOccurance
                });
        }

        [HttpPost]
        public async Task<IActionResult> PostGrade([FromBody] GradeDTO _GradeDTO)
        {
            return await _postHandler(_GradeDTO);
        }

        [HttpPut]
        public async Task<IActionResult> PutGrade([FromBody] GradeDTO _GradeDTO)
        {
            return await _putHandler(_GradeDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGrade([FromQuery] int? SchoolId, [FromQuery] int? StudentId, [FromQuery] int? SectionId, [FromQuery] string? GradeTypeCode, byte? GradeCodeOccurance)
        {
            if (SchoolId != null && StudentId != null && SectionId != null && GradeTypeCode != null && GradeCodeOccurance != null)
                return await _deleteHandler(new GradePK
                {
                    SchoolId = (int)SchoolId,
                    StudentId = (int)StudentId,
                    SectionId = (int)SectionId,
                    GradeTypeCode = GradeTypeCode,
                    GradeCodeOccurance = (byte)GradeCodeOccurance
                });
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Please provide a complete primary key.");
        }
    }
}
