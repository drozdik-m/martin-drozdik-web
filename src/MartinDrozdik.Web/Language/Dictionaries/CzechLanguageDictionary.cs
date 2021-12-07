using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bonsai.Services.LanguageDictionary.Static.Languages;

namespace MartinDrozdik.Web.Language.Dictionaries
{
    public class CzechLanguageDictionary : CzechStaticLanguageDictionary
    {
        static readonly Dictionary<string, string> dictionary = new Dictionary<string, string>()
        {

        };

        public CzechLanguageDictionary() : base(dictionary)
        {

        }
    }
}
