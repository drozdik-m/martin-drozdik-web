using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartinDrozdik.Abstraction.Exceptions.Services.CRUD
{
    public class BadRequestException : ServiceException
    {
        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BadRequestException()
        {
        }
    }
}
