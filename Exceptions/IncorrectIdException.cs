using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.Exceptions
{
    [Serializable]
    public class IncorrectIdException : ApplicationException
    {
        public IncorrectIdException()
        {
        }

        public IncorrectIdException(string message) : base(message)
        {
        }

        public IncorrectIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IncorrectIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
