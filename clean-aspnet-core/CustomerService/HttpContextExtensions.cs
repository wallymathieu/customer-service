using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Customers
{
    public static class HttpContextExtensions
    {
        private static Serializer serializer = new Serializer();
        public static bool TryReadFromXml<T>(this HttpRequest request, out T value)
        {
            using (var stream = request.Body)
            {
                T obj;
                try
                {
                    obj = serializer.Deserialize<T>(stream);

                }
                catch (System.Exception)
                {
                    value = default;
                    return false;
                }

                var results = new List<ValidationResult>();
                if (Validator.TryValidateObject(obj, new ValidationContext(obj), results))
                {
                    value = obj;
                    return true;
                }
                value = default;
                return false;
            }
        }
        public static void WriteXml<T>(this HttpResponse HttpResponse, T obj)
        {
            serializer.Serialize(HttpResponse.Body, obj);
        }
    }
}
