using System;
using System.Collections.Generic;
using System.Text;

namespace MartinDrozdik.Web.Configuration
{
    public class WebSecrets
    {
        /// <summary>
        /// Site key / private key of the recaptcha
        /// </summary>
        public string RecaptchaSecret { get; set; } = string.Empty;
    }
}
