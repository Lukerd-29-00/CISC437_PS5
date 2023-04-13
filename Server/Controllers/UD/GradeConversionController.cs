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
    public class GradeConversionController : BaseController<GradeConversion, GradeConversionDTO, GradeConversionPK>
    {
        public GradeConversionController(DOOROracleContext _DBcontext,
            IOraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        protected override DbSet<GradeConversion> _getContext()
        {
            return _context.GradeConversions;
        }


        protected override Expression<Func<GradeConversion, GradeConversionDTO>> _getDTOExp()
        {
            return sp => new GradeConversionDTO
            {
                SchoolId = sp.SchoolId,
                LetterGrade = sp.LetterGrade,
                GradePoint = sp.GradePoint,
                MaxGrade = sp.MaxGrade,
                MinGrade = sp.MinGrade,
                CreatedBy = sp.CreatedBy,
                CreatedDate = sp.CreatedDate,
                ModifiedBy = sp.ModifiedBy,
                ModifiedDate = sp.ModifiedDate
            };
        }

        protected override void _mutateRecord(GradeConversion original, GradeConversionDTO dto)
        {
            original.LetterGrade = dto.LetterGrade;
            original.GradePoint = dto.GradePoint;
            original.MinGrade  = dto.MinGrade;
            original.MaxGrade = dto.MaxGrade;
        }

        protected override Expression<Func<GradeConversion, bool>> _getPredicate(GradeConversionPK Pkey)
        {
            return sp => (sp.LetterGrade == Pkey.LetterGrade) && (sp.SchoolId == Pkey.SchoolId);
        }

        [HttpGet]
        [Route("GetGradeConversion")]
        public async Task<IActionResult> GetGradeConversion() {
            return await _getHandler();
        }

        [HttpGet]
        [Route("GetGradeConversion/{SchoolId}/{LetterGrade}")]
        public async Task<IActionResult> GetGradeConversionByPK(int SchoolId, string LetterGrade)
        {
            return await _getByPkHandler(new GradeConversionPK { LetterGrade = LetterGrade, SchoolId = SchoolId });
        }

        [HttpPost]
        [Route("PostGradeConversion")]
        public async Task<IActionResult> PostGradeConversion([FromBody] GradeConversionDTO _GradeConversionDTO)
        {
            return await _postHandler(_GradeConversionDTO);
        }

        [HttpPut]
        [Route("PutGradeConversion")]
        public async Task<IActionResult> PutGradeConversion([FromBody] GradeConversionDTO _GradeConversionDTO)
        {
            return await _putHandler(_GradeConversionDTO);
        }

        [HttpDelete]
        [Route("DeleteGradeConversion/{SchoolId}/{LetterGrade}")]
        public async Task<IActionResult> DeleteGradeConversion(int SchoolId, string LetterGrade)
        {
            return await _deleteHandler(new GradeConversionPK { LetterGrade = LetterGrade, SchoolId = SchoolId });
        }
    }
}
