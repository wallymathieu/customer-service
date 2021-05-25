using System;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Threading.Tasks;

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
            XmlRootAttribute rootNs = new XmlRootAttribute
            {
                ElementName = t.Name,
                IsNullable = true
            };
            return rootNs;
        }

        public string Serialize(object o)
        {
            using (var ms = new MemoryStream())
            using (var r = new StreamReader(ms))
            {
                Serialize(ms, o);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                return r.ReadToEnd();
            }
        }
        public void Serialize(Stream stream, object o) => 
            XmlSerializerForType(o.GetType()).Serialize(stream, o);

        public void Serialize<T>(Stream stream, T o) => 
            XmlSerializerForType(typeof(T)).Serialize(stream, o);

        public T Deserialize<T>(Stream input) => 
            (T)XmlSerializerForType(typeof(T)).Deserialize(input);
        public async Task<T> DeserializeAsync<T>(string input)
        {
            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream))
            {
                await streamWriter.WriteAsync(input);
                await streamWriter.FlushAsync();
                stream.Seek(0, SeekOrigin.Begin);
                return Deserialize<T>(stream);
            }
        }
    }
}

