using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace Customers
{
    public class XmlUri : IXmlSerializable
    {
        private Uri _Value;

        public XmlUri() { }
        public XmlUri(Uri source) { _Value = source; }

        public static implicit operator Uri(XmlUri o)
        {
            return o == null ? null : o._Value;
        }

        public static implicit operator XmlUri(Uri o)
        {
            return o == null ? null : new XmlUri(o);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            var value = reader.ReadElementContentAsString();
            _Value = string.IsNullOrEmpty(value) ? null: new Uri(value);
        }

        public void WriteXml(XmlWriter writer)
        {
            if (_Value != null)
            {
                writer.WriteValue(_Value.ToString());
            }
            else
            {
                writer.WriteAttributeString("nil", "xsi", "true");
            }
        }
    }
}

