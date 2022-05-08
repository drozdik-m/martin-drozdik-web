using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartinDrozdik.Abstraction.Exceptions.Services.CRUD
{
    public class DifferentIDsException : ServiceException
    {
        public DifferentIDsException(string message) : base(message)
        {
        }

        public DifferentIDsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DifferentIDsException()
        {
        }
    }
}
