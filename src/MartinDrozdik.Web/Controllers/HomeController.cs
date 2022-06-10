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
using Bonsai.Services.RecaptchaV2;
using Bonsai.Services.Email;
using Bonsai.Utils.String;
using Bonsai.Services.Email.Messages;
using Bonsai.Services.RecaptchaV2.Abstraction;
using Bonsai.Services.Email.Abstraction;
using MartinDrozdik.Data.Models.CV;
using MartinDrozdik.Web.Facades.Models.People;
using MartinDrozdik.Web.Facades.Models.Projects;
using MartinDrozdik.Abstraction.Exceptions.Services.CRUD;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MartinDrozdik.Web.Facades.Models.Blog;

namespace Bonsai.Server.Controllers
{

    public class HomeController : Controller
    {
        readonly ILanguageDictionary languageDictionary;
        readonly ICultureProvider cultureProvider;
        readonly ServerConfiguration serverConfig;
        readonly IRecaptchaV2Validator recaptchaValidator;
        readonly IEmailSender emailSender;
        private readonly WorkExperienceFacade workExperienceFacade;
        private readonly EducationFacade educationFacade;
        private readonly LanguageSkillFacade languageSkillFacade;
        private readonly ProjectTagFacade projectTagFacade;
        private readonly ProjectFacade projectFacade;
        private readonly ProjectMarkdownArticleFacade projectMarkdownArticleFacade;
        private readonly ArticleFacade articleFacade;
        private readonly ArticleTagFacade articleTagFacade;
        private readonly BlogMarkdownArticleFacade blogMarkdownArticleFacade;

        public HomeController(ILanguageDictionary languageDictionary, 
            ICultureProvider cultureProvider,
            ServerConfiguration serverConfig,
            IRecaptchaV2Validator recaptchaValidator,
            IEmailSender emailSender,
            WorkExperienceFacade workExperienceFacade,
            EducationFacade educationFacade,
            LanguageSkillFacade languageSkillFacade,
            ProjectTagFacade projectTagFacade,
            ProjectFacade projectFacade,
            ProjectMarkdownArticleFacade projectMarkdownArticleFacade,
            ArticleFacade articleFacade,
            ArticleTagFacade articleTagFacade,
            BlogMarkdownArticleFacade blogMarkdownArticleFacade
            )
        {
            this.languageDictionary = languageDictionary;
            this.cultureProvider = cultureProvider;
            this.serverConfig = serverConfig;
            this.recaptchaValidator = recaptchaValidator;
            this.emailSender = emailSender;
            this.workExperienceFacade = workExperienceFacade;
            this.educationFacade = educationFacade;
            this.languageSkillFacade = languageSkillFacade;
            this.projectTagFacade = projectTagFacade;
            this.projectFacade = projectFacade;
            this.projectMarkdownArticleFacade = projectMarkdownArticleFacade;
            this.articleFacade = articleFacade;
            this.articleTagFacade = articleTagFacade;
            this.blogMarkdownArticleFacade = blogMarkdownArticleFacade;
        }

        public async Task<IActionResult> Index()
        {
            var workExperiences = await workExperienceFacade.GetAsync();
            var educations = await educationFacade.GetAsync();
            var languageSkill = await languageSkillFacade.GetAsync();
            var projectTags = await projectTagFacade.GetAsync();
            var previewArticles = await articleFacade.GetFirstPublishedAsync(3);

            var model = new IndexPageModel(cultureProvider, languageDictionary,
                workExperiences, educations, languageSkill,
                projectTags,
                previewArticles)
            {

            };

            return View(model);

        }

        [Route("projects")]
        public async Task<IActionResult> Projects()
        {
            var projectTags = await projectTagFacade.GetAsync();

            var model = new ProjectsPageModel(cultureProvider, languageDictionary,
                projectTags, 30)
            {

            };

            return View(model);
        }

        [Route("projects/{id}")]
        public async Task<IActionResult> Project(string id)
        {
            try
            {
                var project = await projectFacade.GetAsync(id);
                project.Content = projectMarkdownArticleFacade.WithUpdatedResourceLinks(project.Content,
                    e => $"{serverConfig.ContentDomain}{e}");

                var model = new ProjectPageModel(cultureProvider, languageDictionary,
                    project)
                {

                };

                return View(model);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [Route("blog")]
        public async Task<IActionResult> Blog()
        {
            var articleTags = await articleTagFacade.GetAsync();

            var model = new BlogPageModel(cultureProvider, languageDictionary,
                articleTags)
            {

            };

            return View(model);
        }

        [Route("blog/{id}")]
        public async Task<IActionResult> Article(string id)
        {
            try
            {
                var article = await articleFacade.GetAsync(id);
                article.Content = blogMarkdownArticleFacade.WithUpdatedResourceLinks(article.Content,
                    e => $"{serverConfig.ContentDomain}{e}");

                var model = new ArticlePageModel(cultureProvider, languageDictionary,
                    article)
                {

                };

                return View(model);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(ContactForm contactForm)
        {
            // ----- Text validations -----
            if (string.IsNullOrWhiteSpace(contactForm.Name))
                return BadRequest("Jméno musí být vyplněno");
            if (contactForm.Name.Length >= 256)
                return BadRequest("Jméno může být dlouhý maximálně 256 znaků");

            if (string.IsNullOrWhiteSpace(contactForm.Email))
                return BadRequest("Email musí být vyplněn");
            if (contactForm.Email.Length >= 128)
                return BadRequest("Email může být dlouhý maximálně 128 znaků");

            if (string.IsNullOrWhiteSpace(contactForm.Message))
                return BadRequest("Zpráva musí být vyplněna");
            if (contactForm.Message.Length >= 3072)
                return BadRequest("Zpráva může být dlouhá maximálně 3072 znaků");

            if (string.IsNullOrWhiteSpace(contactForm.Subject))
                contactForm.Subject = "*no subject*";

            // ----- Recaptcha validation -----
            if (string.IsNullOrWhiteSpace(contactForm.RecaptchaResponse))
                return BadRequest("Validační kód pro reCaptcha musí být vyplněn – byla reCaptcha vyřešena?");
            try
            {
                await recaptchaValidator.ValidateAsync(contactForm.RecaptchaResponse);
            }
            catch(Exception e)
            {
                return BadRequest("Ověření reCaptcha selhalo: " + e.Message);
            }

            // ----- Send the email -----

            try
            {
                var messageContent =
                   $"<p><strong>Jméno a příjmení</strong>: {contactForm.Name}</p>" +
                   $"<p><strong>Email</strong>: {contactForm.Email}</p>" +
                   $"<p><strong>Předmět</strong>: {contactForm.Subject}</p>" +
                   $"<p><strong>Zpráva: </strong></p>" +
                   $"<p>{contactForm.Message?.AsTextWithHTMLBreaks()}</p>";

                var emailMessage = new EmailMessage()
                {
                    Recipient = new MailAddress(serverConfig.MainNotificationRecipient, contactForm.Name, Encoding.UTF8),
                    Content = messageContent,
                    ReplyTo = new[] { new MailAddress(contactForm.Email, contactForm.Name, Encoding.UTF8) },
                    Subject = contactForm.Subject,
                    IsBodyHTML = true
                };

                await emailSender.SendAsync(emailMessage);
            }
            catch (Exception e)
            {
                return BadRequest("Email se nepodařilo odeslat: " + e.Message);
            }

            return Ok();
        }
    }
}
