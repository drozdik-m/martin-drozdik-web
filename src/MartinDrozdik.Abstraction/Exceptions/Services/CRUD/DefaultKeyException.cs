using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartinDrozdik.Abstraction.Exceptions.Services.CRUD
{
    public class DefaultKeyException : ServiceException
    {
        public DefaultKeyException(string message) : base(message)
        {
        }

        public DefaultKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DefaultKeyException()
        {
        }
    }
}
