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
    public class SchoolController : BaseController<School, SchoolDTO, int>
    {

        public SchoolController(DOOROracleContext _DBcontext,
            IOraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        protected override DbSet<School> _getContext()
        {
            return _context.Schools;
        }


        protected override Expression<Func<School, SchoolDTO>> _getDTOExp()
        {
            return sp => new SchoolDTO
            {
                SchoolId = sp.SchoolId,
                SchoolName = sp.SchoolName,
                CreatedBy = sp.CreatedBy,
                CreatedDate = sp.CreatedDate,
                ModifiedBy = sp.ModifiedBy,
                ModifiedDate = sp.ModifiedDate
            };
        }
        protected override void _mutateRecord(School original, SchoolDTO dto)
        {
            original.SchoolName = dto.SchoolName;
        }
        protected override Expression<Func<School, bool>> _getPredicate(int Pkey)
        {
            return sp => sp.SchoolId == Pkey;
        }

        [HttpGet]
        [Route("GetSchool")]
        public async Task<IActionResult> GetSchool()
        {
            return await _getHandler();
        }

        [HttpGet]
        [Route("GetSchool/{SchoolId}")]
        public async Task<IActionResult> GetSchoolByPK(int SchoolId)
        {
                return await _getByPkHandler(SchoolId);
        }

        [HttpPost]
        [Route("PostSchool")]
        public async Task<IActionResult> PostSchool([FromBody] SchoolDTO _SchoolDTO)
        {
            return await _postHandler(_SchoolDTO);
        }

        [HttpPut]
        [Route("PutSchool")]
        public async Task<IActionResult> PutSchool([FromBody] SchoolDTO _SchoolDTO)
        {
            return await _putHandler(_SchoolDTO);
        }

        [HttpDelete]
        [Route("DeleteSchool/{SchoolId}")]
        public async Task<IActionResult> DeleteSchool(int SchoolId)
        {
            return await _deleteHandler(SchoolId);
        }
    }
}