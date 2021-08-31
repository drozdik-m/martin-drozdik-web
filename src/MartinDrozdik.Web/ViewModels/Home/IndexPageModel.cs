using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bonsai.Services.LanguageDictionary.Abstraction;

namespace MartinDrozdik.Web.ViewModels.Home
{
    public class IndexPageModel : ViewModelBase
    {
        public IndexPageModel(ICultureProvider cultureProvider, ILanguageDictionary languageDictionary) 
            : base(cultureProvider, languageDictionary)
        {

        }

        public override string Description => throw new NotImplementedException();

        public override string PageTitle => throw new NotImplementedException();

        public override string[] KeywordsList => throw new NotImplementedException();

        public override string OgImage => "Web/_Pages/Index/IndexOG.png";

        public override PageId PageId => PageId.Index;
    }
}
