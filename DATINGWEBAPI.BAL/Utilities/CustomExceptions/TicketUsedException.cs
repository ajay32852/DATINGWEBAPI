using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{
    public class TicketUsedException : Exception
    {
        public TicketUsedException()
        {

        }

        public TicketUsedException(string message)
         : base(message)
        {
        }

        public TicketUsedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
