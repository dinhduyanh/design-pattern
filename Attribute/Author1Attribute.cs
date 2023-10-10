using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                       System.AttributeTargets.Struct)
]
    public class Author1Attribute : System.Attribute
    {
        private string Name;
        public double Version;

        public Author1Attribute(string name)
        {
            Name = name;
            Version = 1.0;
        }
    }
}
