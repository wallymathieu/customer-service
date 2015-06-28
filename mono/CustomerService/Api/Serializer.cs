using System;
using System.IO;
using System.Xml.Serialization;
using System.Text;

namespace Customers
{
    public class Serializer
    {
        private static readonly string ns = "http://schemas.datacontract.org/2004/07/Customers";

        /// <summary>
        /// Returns a serializer with namespace if the type is part of this assembly.
        /// </summary>
        private static XmlSerializer XmlSerializerForType(Type t)
        {
            return typeof(Serializer).Assembly.Equals(t.Assembly)
                ? new XmlSerializer(t, null,null,GetRootNs(t),ns) 
                : new XmlSerializer(t);
        }

        private static XmlRootAttribute GetRootNs(Type t)
        {
            XmlRootAttribute rootNs =  new XmlRootAttribute();
            rootNs.ElementName = t.Name;
            rootNs.IsNullable = true;
            return rootNs;
        }

        public byte[] Serialize(object o)
        {
            var t = o.GetType();
            using (var ms = new MemoryStream())
            using (var r = new StreamReader(ms))
            {
                var x = XmlSerializerForType(t);
                x.Serialize(ms, o);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                return GetBytes(r.ReadToEnd());
            }
        }

        public T Deserialize<T>(Stream input)
        {
            var t = typeof(T);
            var x = XmlSerializerForType(t);
            return (T)x.Deserialize(input);
        }

        private static byte[] GetBytes(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
    }
}

