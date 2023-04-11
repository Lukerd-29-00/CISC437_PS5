using Microsoft.EntityFrameworkCore;

namespace DOOR.Shared.Utils
{
    public class ErrorHandling
    {
        public static List<OraError> TryDecodeDbUpdateException(DbUpdateException ex, IOraTransMsgs _OraTranslateMsgs)
        {

            if ((ex.InnerException is Microsoft.EntityFrameworkCore.DbUpdateException) ||
                (ex.InnerException is Oracle.ManagedDataAccess.Client.OracleException))
            {
                // This is good, continue
            }
            else
            {
                return null;
            }

            var sqlException =
                (Oracle.ManagedDataAccess.Client.OracleException)ex.InnerException;
            List<OraError> result = new List<OraError>();
            for (int i = 0; i < sqlException.Errors.Count; i++)
            {
                result.Add(new OraError(sqlException.Errors[i].Number, _OraTranslateMsgs.TranslateMsg(sqlException.Errors[i].Message.ToString())));
            }
            return result.Any() ? result : null;
        }

    }
}
