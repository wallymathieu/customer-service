using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace Customers
{
    [XmlType(Namespace = "http://schemas.datacontract.org/2004/07/Customers")]
    public partial class Customer : object
    {
        [XmlElement()]
        public int AccountNumber{ get; set; }
        [XmlElement(IsNullable=true)]
        public string AddressCity{ get; set; }
        [XmlElement(IsNullable=true)]
        public string AddressCountry{ get; set; }
        [XmlElement(IsNullable=true)]
        public string AddressStreet{ get; set; }
        [XmlElement()]
        public string FirstName{ get; set; }
        [XmlElement()]
        public CustomerGender Gender{ get; set; }
        [XmlElement()]
        public string LastName{ get; set; }
        [XmlElement(IsNullable=true)]
        public XmlUri PictureUri{ get; set; }
    }
}