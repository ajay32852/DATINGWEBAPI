using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{
    public class ForgetPasswordTicketExpiredException : Exception
    {
        public ForgetPasswordTicketExpiredException()
        {
        }



        public ForgetPasswordTicketExpiredException(string message)
            : base(message)
        {
        }

        public ForgetPasswordTicketExpiredException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
