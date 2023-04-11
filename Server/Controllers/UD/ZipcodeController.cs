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
    public class ZipcodeController : BaseController<Zipcode, ZipcodeDTO, string>
    {

        public ZipcodeController(DOOROracleContext _DBcontext,
            IOraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }

        protected override DbSet<Zipcode> _getContext()
        {
            return _context.Zipcodes;
        }


        protected override Expression<Func<Zipcode, ZipcodeDTO>> _getDTOExp()
        {
            return sp => new ZipcodeDTO
            {
                Zip = sp.Zip,
                City = sp.City,
                State = sp.State,
                CreatedBy = sp.CreatedBy,
                CreatedDate = sp.CreatedDate,
                ModifiedBy = sp.ModifiedBy,
                ModifiedDate = sp.ModifiedDate,
            };
        }
        protected override void _mutateRecord(Zipcode original, ZipcodeDTO dto)
        {
            original.City = dto.City;
            original.State = dto.State;
        }
        protected override Expression<Func<Zipcode, bool>> _getPredicate(string Pkey)
        {
            return sp => sp.Zip == Pkey;
        }
        [HttpGet]
        public async Task<IActionResult> GetZipcodeByPK([FromQuery] string? Zip)
        {
            if (Zip == null)
                return await _getHandler();
            else
                return await _getByPkHandler(Zip);
        }

        [HttpPost]
        public async Task<IActionResult> PostZipcode([FromBody] ZipcodeDTO _ZipcodeDTO)
        {
            return await _postHandler(_ZipcodeDTO);
        }

        [HttpPut]
        public async Task<IActionResult> PutZipcode([FromBody] ZipcodeDTO _ZipcodeDTO)
        {
            return await _putHandler(_ZipcodeDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteZipcode(string? Zip)
        {
            if (Zip != null)
                return await _deleteHandler(Zip);
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Please provide a complete primary key.");
        }
    }
}