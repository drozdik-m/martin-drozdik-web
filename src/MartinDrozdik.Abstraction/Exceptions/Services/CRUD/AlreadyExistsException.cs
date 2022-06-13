using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartinDrozdik.Abstraction.Exceptions.Services.CRUD
{
    public class AlreadyExistsException : ServiceException
    {
        public AlreadyExistsException(string message) : base(message)
        {
        }

        public AlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public AlreadyExistsException()
        {
        }
    }
}
