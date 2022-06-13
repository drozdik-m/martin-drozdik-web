using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MartinDrozdik.Data.Models.Media.Exceptions
{
    public class FileAlreadyExistsException : Exception
    {
        public FileAlreadyExistsException()
        {
        }

        public FileAlreadyExistsException(string? message) : base(message)
        {
        }

        public FileAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FileAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
