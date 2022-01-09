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

namespace Bonsai.Server.Controllers
{

    public class HomeController : Controller
    {
        const string MAIL_RECIPIENT_KEY = "MainNotificationRecipient";

        readonly ILanguageDictionary languageDictionary;
        readonly ICultureProvider cultureProvider;
        readonly IConfiguration configuration;

        public HomeController(ILanguageDictionary languageDictionary, 
            ICultureProvider cultureProvider, 
            IConfiguration configuration)
        {
            this.languageDictionary = languageDictionary;
            this.cultureProvider = cultureProvider;
            this.configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var indexModel = new IndexPageModel(cultureProvider, languageDictionary)
            {

            };

            return View(indexModel);
        }
    }
}
