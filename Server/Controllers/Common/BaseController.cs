using DOOR.EF.Data;
using DOOR.Shared.DTO;
using DOOR.Shared.Exceptions;
using DOOR.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DOOR.Server.Controllers.Common
{
    public abstract class BaseController<Raw, DTO, PK> : Controller where DTO : IDTO<Raw, PK> where Raw : class
    {

        protected DOOROracleContext _context;
        protected readonly IOraTransMsgs _OraTranslateMsgs;

        public BaseController(DOOROracleContext DBcontext,
    IOraTransMsgs _OraTransMsgs)
        {
            _context = DBcontext;
            _OraTranslateMsgs = _OraTransMsgs;
        }

        protected abstract DbSet<Raw> _getContext();

        //These expressions have to be created in the controller instead of the DTO, because static abstract methods and self types are not supported.
        protected abstract Expression<Func<Raw, DTO>> _getDTOExp();

        protected abstract Expression<Func<Raw, bool>> _getPredicate(PK pkey);

        protected abstract void _mutateRecord(Raw original, DTO dto);

        protected async Task<IActionResult> _getHandler()
        {
            List<DTO> lst = await _getContext()
                .Select(_getDTOExp()).ToListAsync();
            return Ok(lst);
        }

        protected async Task<IActionResult> _getByPkHandler(PK pk)
        {
            DTO? output = await _getContext()
                .Where(_getPredicate(pk))
                .Select(_getDTOExp())
                .FirstOrDefaultAsync();
            return Ok(output);
        }

        protected async Task<IActionResult> _postHandler(DTO _DTO)
        {
            try
            {
                Raw? c = await _getContext().Where(_getPredicate(_DTO.primaryKey())).FirstOrDefaultAsync();

                if (c == null)
                {
                    _getContext().Add(_DTO.ToRecord());
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }
        protected async Task<IActionResult> _putHandler(DTO _DTO)
        {
            try
            {
                Raw? c = _getContext().Where(_getPredicate(_DTO.primaryKey())).FirstOrDefault();
                if (c != null)
                {
                    _mutateRecord(c,_DTO);
                    await _context.SaveChangesAsync();
                    return Ok(null);
                }
                return StatusCode(StatusCodes.Status404NotFound);

            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }
        }


        protected async Task<IActionResult> _deleteHandler(PK pkey)
        {
            try
            {
                Raw? c = await _getContext().Where(_getPredicate(pkey)).FirstOrDefaultAsync();

                if (c != null)
                {
                    _getContext().Remove(c);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }
    }
}
