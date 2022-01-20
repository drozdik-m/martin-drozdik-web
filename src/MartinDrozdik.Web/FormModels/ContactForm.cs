using System;
using System.Collections.Generic;
using System.Text;

namespace Bonsai.Models.Forms
{
    public class ContactForm
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string RecaptchaResponse { get; set; } = string.Empty;
    }
}
