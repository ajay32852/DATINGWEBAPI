using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{
    public class UserDuplicateException : Exception
    {
        public UserDuplicateException()
        {

        }


        public UserDuplicateException(string message)
            : base(message)
        {

        }

        public UserDuplicateException(string message, Exception inner)
          : base(message, inner)
        {

        }


    }
}