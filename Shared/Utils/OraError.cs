﻿namespace DOOR.Shared.Utils
{
    public class OraError
    {
        public OraError()
        {

        }
        public OraError(int oraErrorNumber, string oraErrorMsg)
        {
            this.OraErrorMsg = oraErrorMsg;
            this.OraErrorNumber = oraErrorNumber;
        }
        public int OraErrorNumber { get; set; }
        public string OraErrorMsg { get; set; }
        public bool bOraTranslated { get; set; }
    }
}
