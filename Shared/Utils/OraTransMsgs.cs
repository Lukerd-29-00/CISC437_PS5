using DOOR.EF.Data;
using DOOR.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace DOOR.Shared.Utils
{
    public class OraTransMsgs : IOraTransMsgs
    {
        private DbContextOptions<DOOROracleContext> _dbContextOptions;
        public List<OraTranslateMsg> lstOraTranslateMsgs { get; set; }

        public OraTransMsgs(DbContextOptions<DOOROracleContext> dbContextOptions)
        {
            this._dbContextOptions = dbContextOptions;
            LoadMsgs();
        }

        public void LoadMsgs()
        {
            using (var db = new DOOROracleContext(_dbContextOptions))
            {

                lstOraTranslateMsgs = db.OraTranslateMsgs.ToList();
            }
        }

        public string TranslateMsg(string strMessage)
        {

            foreach (var msg in lstOraTranslateMsgs)
            {
                if (strMessage.ToUpper().Contains(msg.OraConstraintName.ToUpper()))
                {
                    return msg.OraErrorMessage;
                }
            }
            return strMessage;
        }
    }
}
