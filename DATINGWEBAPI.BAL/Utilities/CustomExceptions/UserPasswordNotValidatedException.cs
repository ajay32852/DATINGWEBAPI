using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{
    public class UserPasswordNotValidatedException : Exception
    {
        public UserPasswordNotValidatedException()
        {

        }
        public UserPasswordNotValidatedException(string message)
       : base(message)
        {

        }

        public UserPasswordNotValidatedException(string message, Exception inner)
      : base(message, inner)
        {

        }
    }
}
