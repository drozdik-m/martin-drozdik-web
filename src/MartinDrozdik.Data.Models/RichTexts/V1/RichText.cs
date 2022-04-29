using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartinDrozdik.Data.Models.RichTexts.V1
{
    public class RichText
    {
        /// <summary>
        /// The version of this format
        /// </summary>
        public Version Version => new(1, 0, 0);

        /// <summary>
        /// Content blocks of this text
        /// </summary>
        public List<RichTextBlock> Blocks { get; private set; } = new List<RichTextBlock>();


    }
}
