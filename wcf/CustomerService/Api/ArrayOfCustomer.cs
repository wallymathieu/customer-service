using System.Runtime.Serialization;

namespace Customers
{
    [DataContract(Name = "ArrayOfCustomer", Namespace = "http://schemas.datacontract.org/2004/07/Customers")]
    public class ArrayOfCustomer
    {
        public ArrayOfCustomer()
        {
        }
        [DataMember]
        public  Customer[] Customer
        {
            get ;
            set ;
        }
    }
}

