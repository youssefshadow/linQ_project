using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQ_project.Models
{
    public class ConversionResult
    {
        public List<Dictionary<string, object>> Records { get; set; } 
        public List<DataField> Fields { get; set; } 

        public ConversionResult()
        {
            Records = new List<Dictionary<string, object>>();
            Fields = new List<DataField>();
        }
    }
}
