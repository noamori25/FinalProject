using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.Exceptions
{
    [Serializable]
    public class CountryInUseException : ApplicationException
    {
        public CountryInUseException()
        {
        }

        public CountryInUseException(string message) : base(message)
        {
        }

        public CountryInUseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CountryInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
