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
        protected override void _mutateRecord(Grade original, GradeDTO dto)
        {
            original.NumericGrade = dto.NumericGrade;
            original.Comments = dto.Comments;
        }
        protected override Expression<Func<Grade, bool>> _getPredicate(GradePK Pkey)
        {
            return sp => (sp.SchoolId == Pkey.SchoolId) && (sp.StudentId == Pkey.StudentId) && (Pkey.SectionId == sp.SectionId) && (Pkey.GradeTypeCode == sp.GradeTypeCode) && (Pkey.GradeCodeOccurance == sp.GradeCodeOccurrence);
        }

        [HttpGet]
        [Route("GetGrade")]
        public async Task<IActionResult> GetGrade()
        {
            return await _getHandler();
        }

        [HttpGet]
        [Route("GetGrade/{SchoolId}/{GradeTypeCode}/{GradeCodeOccurance}/{SectionId}/{StudentId}")]
        public async Task<IActionResult> GetGradeByPK(int SchoolId, string GradeTypeCode, byte GradeCodeOccurance, int SectionId, int StudentId)
        {
            return await _getByPkHandler(new GradePK
            {
                SchoolId = SchoolId,
                StudentId = StudentId,
                SectionId = SectionId,
                GradeTypeCode = GradeTypeCode,
                GradeCodeOccurance = GradeCodeOccurance
            });
        }

        [HttpPost]
        [Route("PostGrade")]
        public async Task<IActionResult> PostGrade([FromBody] GradeDTO _GradeDTO)
        {
            return await _postHandler(_GradeDTO);
        }

        [HttpPut]
        [Route("PutGrade")]
        public async Task<IActionResult> PutGrade([FromBody] GradeDTO _GradeDTO)
        {
            return await _putHandler(_GradeDTO);
        }

        [HttpDelete]
        [Route("DeleteGrade/{SchoolId}/{GradeTypeCode}/{GradeCodeOccurance}/{SectionId}/{StudentId}")]
        public async Task<IActionResult> DeleteGrade(int SchoolId, string GradeTypeCode, byte GradeCodeOccurance, int SectionId, int StudentId)
        {
            return await _deleteHandler(new GradePK
            {
                SchoolId = SchoolId,
                StudentId = StudentId,
                SectionId = SectionId,
                GradeTypeCode = GradeTypeCode,
                GradeCodeOccurance = GradeCodeOccurance
            });
        }
    }
}
