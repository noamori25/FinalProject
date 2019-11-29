using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.Exceptions
{
    [Serializable]
    public class TicketsSoldOutException : ApplicationException
    {
        public TicketsSoldOutException()
        {
        }

        public TicketsSoldOutException(string message) : base(message)
        {
        }

        public TicketsSoldOutException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TicketsSoldOutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
