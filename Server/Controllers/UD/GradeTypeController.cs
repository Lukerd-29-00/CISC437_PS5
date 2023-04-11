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
    public class GradeTypeController : BaseController<GradeType, GradeTypeDTO, GradeTypePK>
    {

        public GradeTypeController(DOOROracleContext _DBcontext,
            IOraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        protected override DbSet<GradeType> _getContext()
        {
            return _context.GradeTypes;
        }


        protected override Expression<Func<GradeType, GradeTypeDTO>> _getDTOExp()
        {
            return sp => new GradeTypeDTO
            {
                SchoolId = sp.SchoolId,
                GradeTypeCode = sp.GradeTypeCode,
                Description = sp.Description,
                CreatedBy = sp.CreatedBy,
                CreatedDate = sp.CreatedDate,
                ModifiedBy = sp.ModifiedBy,
                ModifiedDate = sp.ModifiedDate
            };
        }
        protected override void _mutateRecord(GradeType original, GradeTypeDTO dto)
        {
            original.Description = dto.Description;
        }
        protected override Expression<Func<GradeType, bool>> _getPredicate(GradeTypePK Pkey)
        {
            return sp => (sp.GradeTypeCode == Pkey.GradeTypeCode) && (sp.SchoolId == Pkey.SchoolId);
        }
        [HttpGet]
        public async Task<IActionResult> GetGradeTypeByPK([FromQuery] string? GradeTypeCode, [FromQuery] int? SchoolId)
        {
            if (GradeTypeCode == null && SchoolId == null)
                return await _getHandler();
            else if (GradeTypeCode == null || SchoolId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Please send a complete primary key");
            else
                return await _getByPkHandler(new GradeTypePK { GradeTypeCode = GradeTypeCode, SchoolId = (int)SchoolId });
        }

        [HttpPost]
        public async Task<IActionResult> PostGradeType([FromBody] GradeTypeDTO _GradeTypeDTO)
        {
            return await _postHandler(_GradeTypeDTO);
        }

        [HttpPut]
        public async Task<IActionResult> PutGradeType([FromBody] GradeTypeDTO _GradeTypeDTO)
        {
            return await _putHandler(_GradeTypeDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGradeType([FromQuery] string? GradeTypeCode, [FromQuery] int? SchoolId)
        {
            if (GradeTypeCode != null && SchoolId != null)
                return await _deleteHandler(new GradeTypePK { GradeTypeCode = GradeTypeCode, SchoolId = (int)SchoolId });
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Please provide a complete primary key.");
        }
    }
}