using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bonsai.Services.LanguageDictionary.Abstraction;
using MartinDrozdik.Web.ViewModels.Home;

namespace MartinDrozdik.Web.ViewModels
{
    public abstract class ViewModelBase
    {
        /// <summary>
        /// Culture provider for this page
        /// </summary>
        public ICultureProvider CultureProvider { get; }

        /// <summary>
        /// Language provider for this page
        /// </summary>
        public ILanguageDictionary LanguageDictionary { get; }

        public ViewModelBase(ICultureProvider cultureProvider, ILanguageDictionary languageDictionary)
        {
            if (cultureProvider is null)
                throw new ArgumentNullException(nameof(cultureProvider));

            if (languageDictionary is null)
                throw new ArgumentNullException(nameof(languageDictionary));

            CultureProvider = cultureProvider;
            LanguageDictionary = languageDictionary;
        }

        /// <summary>
        /// Description of this page
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Keywords string (k1, k2, ...) for this page
        /// </summary>
        public virtual string Keywords => string.Join(", ", KeywordsList);

        /// <summary>
        /// Full title of this page (f.e. Index | Company | #WeDaBest)
        /// </summary>
        public virtual string Title => PageTitle + TitlePostfix;

        /// <summary>
        /// Base title of this page (f.e. Blog)
        /// </summary>
        public abstract string PageTitle { get; }

        /// <summary>
        /// Title postix that is applied to titles (f.e. " | Company | #WeDaBest")
        /// </summary>
        public virtual string TitlePostfix => " | Martin Drozdík | Portfolio";

        /// <summary>
        /// List of keywords for this page
        /// </summary>
        public abstract string[] KeywordsList { get; }

        /// <summary>
        /// Name of the client (f.e. Bonsai Development)
        /// </summary>
        public virtual string ClientName { get; } = "Martin Drozdík";

        /// <summary>
        /// Name of the site (f.e. Bonsai Development Blog)
        /// </summary>
        public virtual string SiteName { get; } = "Martin Drozdík Portfolio";

        /// <summary>
        /// Path to the OG image
        /// </summary>
        public abstract string OgImage { get; }

        /// <summary>
        /// Type of this page
        /// </summary>
        public virtual string PageType { get; } = "website";

        /// <summary>
        /// Id of this page, f.e. "index"
        /// </summary>
        public abstract PageId PageId { get; }
    }
}
