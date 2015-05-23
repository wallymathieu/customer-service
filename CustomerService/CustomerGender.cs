using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customers
{

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "CustomerGender", Namespace = "http://schemas.datacontract.org/2004/07/Customers")]
    public enum CustomerGender : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Male = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Female = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Boy = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Girl = 3,
    }
}