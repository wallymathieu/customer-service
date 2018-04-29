using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Customers
{
     public class ArrayOfCustomer
    {
        public ArrayOfCustomer()
        {
        }
        
        public  Customer[] Customer
        {
            get ;
            set ;
        }
    }
}

