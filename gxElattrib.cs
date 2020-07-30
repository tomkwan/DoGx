using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoGx
{
    public class gxElattrib: Attribute
    {
        public string TagName
        {
            get;
            set;
        }

        public string prefix
        {
            get;
            set;

        }
    }
}
