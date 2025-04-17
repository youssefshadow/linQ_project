using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQ_project.Models
{
    public class DataSource
    {
        public string SourceType { get; set; } // JSON, XML, DB
        public string FilePath { get; set; }   // l'uril  du fichier

        public DataSource(string sourceType, string filePath)
        {
            SourceType = sourceType;
            FilePath = filePath;
        }
    }
}
