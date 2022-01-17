using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Bonsai.Services.LanguageDictionary.Abstraction;
using MartinDrozdik.Web.Views.Home;
using MartinDrozdik.Web.Configuration;
using Bonsai.Models.Forms;

namespace Bonsai.Server.Controllers
{

    public class HomeController : Controller
    {
        readonly ILanguageDictionary languageDictionary;
        readonly ICultureProvider cultureProvider;
        readonly WebConfiguration webConfig;
        readonly WebSecrets webSecrets;

        public HomeController(ILanguageDictionary languageDictionary, 
            ICultureProvider cultureProvider, 
            WebConfiguration webConfig,
            WebSecrets webSecrets
            )
        {
            this.languageDictionary = languageDictionary;
            this.cultureProvider = cultureProvider;
            this.webConfig = webConfig;
            this.webSecrets = webSecrets;
        }

        public async Task<IActionResult> Index()
        {
            var indexModel = new IndexPageModel(cultureProvider, languageDictionary)
            {

            };

            return View(indexModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(ContactForm contactForm)
        {
            //TODO validation (max chars etc)

            //TODO recaptcha validation

            //TODO send mail to myself

            return Ok();
        }
    }
}
