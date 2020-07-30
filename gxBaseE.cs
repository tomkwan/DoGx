using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tomflection;
namespace DoGx
{
    public abstract class gxBaseE<T> : XmlElement, IPropertyStorable  where T: gxBaseE<T>
    {
        private Dictionary<string, object> _PropertyStorage = new Dictionary<string, object>();

        protected internal gxBaseE(string prefix, string localName, string namespaceURI, XmlDocument doc) : base(prefix, localName, namespaceURI, doc)
        {
        }

        public IEnumerable<T> ChildrenEs =>
            this.ChildNodes.Cast<XmlNode>().Where(
                    n => n.NodeType == XmlNodeType.Element).Cast<T>();

        public Dictionary<string, object> PropertyStorage
        {
            get
            {
                return this._PropertyStorage;
            }
        }

        public gxDocBase<T> ParentGxDoc
        {
            get
            {
                return this.OwnerDocument as gxDocBase<T>;
            }
        }

    }
}
