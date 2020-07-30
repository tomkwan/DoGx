using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using Tomflection;
using System.Xml.Linq;

namespace DoGx
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="CLASS_T">Base CLASS the retrieved class should be inherited to ( inclusive )</typeparam>
    public abstract class gxElfactoryBase<CLASS_T> 
    {
        protected Dictionary<string, TypeN_Attr<gxElattrib>> ElDict;

        //Assembly assm;
        //string TargetNS;

        public gxElfactoryBase(Assembly assm, string TargetNS)
        {
            //this.assm = assm;
            //this.TargetNS = TargetNS;

            var lt = assm.getTypeN_Attrs<gxElattrib, CLASS_T>( TargetNS );
            ElDict = lt.ToDictionary<TypeN_Attr<gxElattrib>, string >(k=> k.attr.TagName );
        }


        public abstract Dictionary<string, string> NSDict
        {
            get;
        }


        public abstract XmlElement CreateElement(string prefix, string localName, string namespaceURI, XmlDocument doc);

        protected virtual XmlElement CreateByTagName(string prefix, string localName, string namespaceURI, XmlDocument doc)
        {
            if (!this.ElDict.ContainsKey(localName))
                return null;
                //throw new Excps.TagNameNotFoundExcept("TagName not found in dict --> " + localName);
            
            return invokeInstance( 
                this.ElDict[localName].t,
                prefix, localName, namespaceURI, doc);

        }

        protected XmlElement invokeInstance(Type EleType, string prefix, string localName, string namespaceURI, XmlDocument doc)
        {
            Type stringT = typeof(string);
            Type XmlDocT = typeof(XmlDocument);

            ConstructorInfo ci = 
            EleType.GetConstructor(new Type[] 
            {
                stringT, stringT, stringT, XmlDocT
            });

            XmlElement xe = ci.Invoke(new object[] { prefix, localName, namespaceURI, doc }) as XmlElement;
            return xe;

        }

        public virtual XElement XView
        {
            get
            {
                XElement xe =
                    new XElement ("rrot",
                        from v in this.ElDict.Values
                         select
                         new  XElement( "item",
                              new XAttribute (v.attr.TagName, v.t.Name)));

                return xe;




                
            }
        }
    }
}
