using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace Customers
{
    public partial class Customer : object
    {
        public int AccountNumber{ get; set; }
        public string AddressCity{ get; set; }
        public string AddressCountry{ get; set; }
        public string AddressStreet{ get; set; }
        public string FirstName{ get; set; }
        public CustomerGender Gender{ get; set; }
        public string LastName{ get; set; }
        public Uri PictureUri{ get; set; }
    }
}