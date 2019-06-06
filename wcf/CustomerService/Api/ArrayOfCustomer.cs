using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Customers
{
    [XmlRoot(Namespace = "http://schemas.datacontract.org/2004/07/Customers")]
    [XmlType(Namespace="http://schemas.datacontract.org/2004/07/Customers")]
     public class ArrayOfCustomer
    {
        public ArrayOfCustomer()
        {
        }

        [XmlElement("Customer")]
        public  Customer[] Customer
        {
            get ;
            set ;
        }
    }
}

