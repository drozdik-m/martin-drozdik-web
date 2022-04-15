using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartinDrozdik.Data.Models.Files
{
    public class UploadFileData
    {
        public byte[] Bytes { get; set; } = Array.Empty<byte>();

        public string FileName { get; set; } = string.Empty;

        public string FileType { get; set; } = string.Empty;

        public long FileSize { get; set; }
    }
}
