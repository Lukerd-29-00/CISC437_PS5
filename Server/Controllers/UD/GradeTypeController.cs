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
        [Route("GetGradeType")]
        public async Task<IActionResult> GetGradeType()
        {
            return await _getHandler();
        }

        [HttpGet]
        [Route("GetGradeType/{SchoolId}/{GradeTypeCode}")]
        public async Task<IActionResult> GetGradeTypeByPK(int SchoolId, string GradeTypeCode)
        {

            return await _getByPkHandler(new GradeTypePK { GradeTypeCode = GradeTypeCode, SchoolId = SchoolId });
        }

        [HttpPost]
        [Route("PostGradeType")]
        public async Task<IActionResult> PostGradeType([FromBody] GradeTypeDTO _GradeTypeDTO)
        {
            return await _postHandler(_GradeTypeDTO);
        }

        [HttpPut]
        [Route("PutGradeType")]
        public async Task<IActionResult> PutGradeType([FromBody] GradeTypeDTO _GradeTypeDTO)
        {
            return await _putHandler(_GradeTypeDTO);
        }

        [HttpDelete]
        [Route("DeleteGradeType/{SchoolId}/{GradeTypeCode}")]
        public async Task<IActionResult> DeleteGradeType(int SchoolId, string GradeTypeCode)
        {
            return await _deleteHandler(new GradeTypePK { GradeTypeCode = GradeTypeCode, SchoolId = SchoolId });

        }
    }
}