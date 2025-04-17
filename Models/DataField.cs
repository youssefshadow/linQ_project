using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQ_project.Models
{
    public class DataField
    {
        public string Name { get; set; } 
        public Type FieldType { get; set; } 
        public bool IsSelected { get; set; } 

        public DataField(string name, Type fieldType, bool isSelected = true)
        {
            Name = name;
            FieldType = fieldType;
            IsSelected = isSelected;
        }
    }
}
