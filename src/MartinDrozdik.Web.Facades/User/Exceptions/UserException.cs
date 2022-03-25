using System;
using System.Collections.Generic;
using System.Text;

namespace MartinDrozdik.Web.Facades.User.Exceptions
{
    public class UserException : Exception
    {
        public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();

        public UserException()
        {

        }

        public UserException(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
