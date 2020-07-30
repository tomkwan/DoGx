using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DoGx
{
    /// <summary>
    /// XmlDocument with Namespaces, provided in a Dict
    /// </summary>
    public class NSxDocBase : XmlDocument
    {
        public XmlNamespaceManager NSG;

        public NSxDocBase(IEnumerable < Dictionary<string, string>> NamespaceList)
        {
            this.NSG = new XmlNamespaceManager(this.NameTable);
            foreach (var namespaces in NamespaceList)
            {
                foreach (string k in namespaces.Keys)
                {
                    NSG.AddNamespace(k, namespaces[k]);
                }
            }

        }
    }
}
