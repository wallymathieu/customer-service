using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
namespace Customers
{
    [XmlType(TypeName = "CustomerGender", Namespace = "http://schemas.datacontract.org/2004/07/Customers")]
    public enum CustomerGender : int
    {
        Male = 0,
        Female = 1,
        Boy = 2,
        Girl = 3,
    }
}