using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ganss.XSS;

namespace MartinDrozdik.Data.Models.Text
{
    //TODO Paragraph
    //TODO Text with side line
    //TODO Heading
    //TODO Quote
    //TODO Image
    //TODO Separator
    //TODO Ordered list
    //TODO Unordered list
    //TODO Youtube video
    //TODO Raw HTML
    //TODO Code
    //TODO File

    public abstract class RichTextBlock
    {

        /// <summary>
        /// Name of this block
        /// </summary>
        public abstract string TypeName { get; }

        /// <summary>
        /// Sanitizes the input
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected static string Sanitized(string text)
        {
            return sanitizer.Sanitize(text);
        }
        private static HtmlSanitizer sanitizer = new();

        /// <summary>
        /// Translates this block into HTML
        /// </summary>
        /// <returns></returns>
        public abstract string ToHTML();


    }
}
