﻿using DOOR.Shared.Utils;
using System.Net;

namespace DOOR.Shared.Exceptions
{
    public class CustomOraException : Exception
    {
        public List<OraError> _ValidationResult = new List<OraError>();
        public HttpStatusCode _HttpStatusCode;

        public CustomOraException(List<OraError> _Errors, HttpStatusCode _HttpStatusCode)
        {
            this._ValidationResult = _Errors;
            this._HttpStatusCode = _HttpStatusCode;
        }

    }
}
