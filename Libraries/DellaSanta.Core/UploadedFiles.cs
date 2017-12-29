using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DellaSanta.Core
{
    public class UploadedFiles
    {

        public UploadedFiles()
        {
          
        }
        public int UploadedFilesId { get; set; }
        public string Name { get; set; }
        public string NameOnDisk { get; set; }
        public bool IsProcessed { get; set; }



    }
}
