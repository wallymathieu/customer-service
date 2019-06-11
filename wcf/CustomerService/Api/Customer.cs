using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace Customers
{
    [DataContract(Name = "Customer", Namespace = "http://schemas.datacontract.org/2004/07/Customers")]
    public partial class Customer : object
    {
        [DataMember]
        public int AccountNumber{ get; set; }
        [DataMember]
        public string AddressCity{ get; set; }
        [DataMember]
        public string AddressCountry{ get; set; }
        [DataMember]
        public string AddressStreet{ get; set; }
        [DataMember]
        public string FirstName{ get; set; }
        [DataMember]
        public CustomerGender Gender{ get; set; }
        [DataMember]
        public string LastName{ get; set; }
        [DataMember]
        public Uri PictureUri{ get; set; }
    }
}