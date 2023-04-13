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
        protected override void _mutateRecord(Section original, SectionDTO dto)
        {
            original.SectionNo = dto.SectionNo;
            original.CourseNo = dto.CourseNo;
            original.StartDateTime = dto.StartDateTime;
            original.Location = dto.Location;
            original.InstructorId = dto.InstructorId;
            original.Capacity = dto.Capacity;
        }
        protected override Expression<Func<Section, bool>> _getPredicate(SectionPK Pkey)
        {
            return sp => (sp.SectionId == Pkey.SectionId) && (sp.SchoolId == Pkey.SchoolId);
        }

        [HttpGet]
        [Route("GetSection")]
        public async Task<IActionResult> GetSection()
        {
            return await _getHandler();
        }

        [HttpGet]
        [Route("GetSection/{SchoolId}/{SectionId}")]
        public async Task<IActionResult> GetSectionByPK(int SchoolId, int SectionId)
        {
                return await _getByPkHandler(new SectionPK { SectionId = SectionId, SchoolId = SchoolId });
        }

        [HttpPost]
        [Route("PostSection")]
        public async Task<IActionResult> PostSection([FromBody] SectionDTO _SectionDTO)
        {
            return await _postHandler(_SectionDTO);
        }

        [HttpPut]
        [Route("PutSection")]
        public async Task<IActionResult> PutSection([FromBody] SectionDTO _SectionDTO, [FromQuery] int? SectionId, [FromQuery] int? SchoolId)
        {
            if (SectionId != null && SchoolId != null)
                return await _putHandler(_SectionDTO);
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Please provide a complete primary key through the query string.");
        }

        [HttpDelete]
        [Route("DeleteSection/{SchoolId}/{SectionId}")]
        public async Task<IActionResult> DeleteSection(int SchoolId, int SectionId)
        {
            return await _deleteHandler(new SectionPK { SectionId = SectionId, SchoolId = SchoolId });
        }
    }
}