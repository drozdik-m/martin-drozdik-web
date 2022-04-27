using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartinDrozdik.Data.Models.Text
{
    public class RichText
    {
        public List<RichTextBlock> Blocks { get; private set; } = new List<RichTextBlock>();
    }
}
