using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlMaker
{
    struct XmlData
    {
        public string ElementName { get; set; }
        public Dictionary<string,string> Attributes { get; set; }
        public List<XmlData> ChildNodes { get; set; }
    }
}
