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
    public class SectionController : BaseController<Section, SectionDTO, SectionPK>
    {
        public SectionController(DOOROracleContext _DBcontext,
            IOraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        protected override DbSet<Section> _getContext()
        {
            return _context.Sections;
        }


        protected override Expression<Func<Section, SectionDTO>> _getDTOExp()
        {
            return sp => new SectionDTO
            {
                SectionId = sp.SectionId,
                SectionNo = sp.SectionNo,
                SchoolId = sp.SchoolId,
                CourseNo = sp.CourseNo,
                StartDateTime = sp.StartDateTime,
                Location = sp.Location,
                InstructorId = sp.InstructorId,
                Capacity = sp.Capacity,
                CreatedBy = sp.CreatedBy,
                CreatedDate = sp.CreatedDate,
                ModifiedBy = sp.ModifiedBy,
                ModifiedDate = sp.ModifiedDate,
            };
        }

        protected override Expression<Func<Section, bool>> _getPredicate(SectionPK Pkey)
        {
            return sp => (sp.SectionId == Pkey.SectionId) && (sp.SchoolId == Pkey.SchoolId);
        }
        [HttpGet]
        public async Task<IActionResult> GetSectionByPK([FromQuery] int? SectionId, [FromQuery] int? SchoolId)
        {
            if (SectionId == null && SchoolId == null)
                return await _getHandler();
            else if (SectionId == null || SchoolId == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Please send a complete primary key");
            else
                return await _getByPkHandler(new SectionPK { SectionId = (int)SectionId, SchoolId = (int)SchoolId });
        }

        [HttpPost]
        public async Task<IActionResult> PostSection([FromBody] SectionDTO _SectionDTO)
        {
            return await _postHandler(_SectionDTO);
        }

        [HttpPut]
        public async Task<IActionResult> PutSection([FromBody] SectionDTO _SectionDTO, [FromQuery] int? SectionId, [FromQuery] int? SchoolId)
        {
            if (SectionId != null && SchoolId != null)
                return await _putHandler(_SectionDTO);
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Please provide a complete primary key through the query string.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSection([FromQuery] int? SectionId, [FromQuery] int? SchoolId)
        {
            if (SectionId != null && SchoolId != null)
                return await _deleteHandler(new SectionPK { SectionId = (int)SectionId, SchoolId = (int)SchoolId });
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Please provide a complete primary key.");
        }
    }
}