using System;
using System.Collections.Generic;
using System.Text;
using MartinDrozdik.Abstraction.Entities;

namespace MartinDrozdik.Abstraction.Localization
{
    public interface ILanguage : IIdentifiable<int>, INameable
    {
        /// <summary>
        /// Two letter ISO code that represents the language
        /// </summary>
        string IsoCode { get; set; }
    }
}
