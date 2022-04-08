using System;
using System.Collections.Generic;
using System.Text;
using Bonsai.Services.Email.Configuration;
using Bonsai.Services.RecaptchaV2.Configuration;
using MartinDrozdik.Data.DbContexts.Configuration;

namespace MartinDrozdik.Web.Configuration
{
    public class ServerConfiguration
    {
        /// <summary>
        /// Configuration regarding the website
        /// </summary>
        public WebConfiguration Web { get; set; }

        /// <summary>
        /// The domain where the website is currently running
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Email configuration for a sending email account
        /// </summary>
        public EmailSenderConfiguration EmailSender { get; set; }

        /// <summary>
        /// Recaptcha configuration
        /// </summary>
        public RecaptchaV2Configuration Recaptcha { get; set; }

        /// <summary>
        /// Email that should recieve all important notifications
        /// </summary>
        public string MainNotificationRecipient { get; set; }

        /// <summary>
        /// Connection strings setup
        /// </summary>
        public ConnectionStringsConfiguration ConnectionStrings { get; set; }
    }
}
