using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOOR.Shared.Exceptions
{
    public class NoRecordException : Exception
    {
        public NoRecordException(string message) : base(message) { }
    }
}
