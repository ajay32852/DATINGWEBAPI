using System.Net;

namespace DATINGWEBAPI.BLL.Utilities.CustomExceptions
{
    public class BrokerIPBlocked : Exception
    {
        public BrokerIPBlocked()
        {
                
        }
        public BrokerIPBlocked(string message)
            : base(message)
        {

        }

        public BrokerIPBlocked(string message, Exception inner)
         : base(message,inner)
        {

        }
    }
}
