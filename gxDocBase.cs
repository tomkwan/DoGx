using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DoGx
{
    public class gxDocBase<CLASS_T> : NSxDocBase where CLASS_T: XmlElement
    {
        public IEnumerable <gxElfactoryBase<CLASS_T>> ElFactories;
        

        public gxDocBase(IEnumerable<gxElfactoryBase<CLASS_T>> ElFactories) 
            :base( ElFactories.Select ( p => p.NSDict) )
        {

            this.ElFactories= ElFactories;
        }


        public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
        {
            foreach (var factory in this.ElFactories)
            {
                XmlElement xe = factory.CreateElement(prefix, localName, namespaceURI, this);
                if (xe != null)
                    return xe;
            }


            throw new Excps.TagNameNotFoundExcept("TagName not found in dict --> " + localName);

            //if (xe == null)
            //    return base.CreateElement(prefix, localName, namespaceURI);
            //else
            //    return xe;

        }

        CLASS_T _root;
        public CLASS_T root
        {
            get
            {
                if (_root == null)
                    _root = DocumentElement as CLASS_T;

                return _root;
            }
        }



    }
}
