using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartinDrozdik.Data.Models.Text.Blocks
{
    public class ImportantTextBlock : RichTextBlock
    {
        /// <inheritdoc/>
        public override string TypeName => "Important text";


        /// <summary>
        /// The important text to show
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <inheritdoc/>
        public override string ToHTML()
        {
            return $"<p class=\"impotantText\">{Sanitized(Text)}</p>";
        }
    }
}
