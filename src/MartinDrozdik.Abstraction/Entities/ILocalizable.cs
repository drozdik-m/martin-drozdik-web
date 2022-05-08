using System;
using System.Collections.Generic;
using System.Text;
using MartinDrozdik.Abstraction.Localization;

namespace MartinDrozdik.Abstraction.Entities
{
    public interface ILocalizable
    {
        /// <summary>
        /// Language of this entity
        /// </summary>
        ILanguage Language { get; }
    }
}
