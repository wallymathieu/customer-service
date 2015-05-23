using System.Runtime.Serialization;
namespace Customers
{
    [DataContract(Name = "Customer", Namespace = "http://schemas.datacontract.org/2004/07/Customers")]
    public partial class Customer : object, IExtensibleDataObject
    {
        private ExtensionDataObject extensionDataField;

        private int AccountNumberField;

        private string AddressCityField;

        private string AddressCountryField;

        private string AddressStreetField;

        private string FirstNameField;

        private Customers.CustomerGender GenderField;

        private string LastNameField;

        private System.Uri PictureUriField;

        public ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [DataMember()]
        public int AccountNumber
        {
            get
            {
                return this.AccountNumberField;
            }
            set
            {
                this.AccountNumberField = value;
            }
        }

        [DataMember()]
        public string AddressCity
        {
            get
            {
                return this.AddressCityField;
            }
            set
            {
                this.AddressCityField = value;
            }
        }

        [DataMember()]
        public string AddressCountry
        {
            get
            {
                return this.AddressCountryField;
            }
            set
            {
                this.AddressCountryField = value;
            }
        }

        [DataMember()]
        public string AddressStreet
        {
            get
            {
                return this.AddressStreetField;
            }
            set
            {
                this.AddressStreetField = value;
            }
        }

        [DataMember()]
        public string FirstName
        {
            get
            {
                return this.FirstNameField;
            }
            set
            {
                this.FirstNameField = value;
            }
        }

        [DataMember()]
        public CustomerGender Gender
        {
            get
            {
                return this.GenderField;
            }
            set
            {
                this.GenderField = value;
            }
        }

        [DataMember()]
        public string LastName
        {
            get
            {
                return this.LastNameField;
            }
            set
            {
                this.LastNameField = value;
            }
        }

        [DataMember()]
        public System.Uri PictureUri
        {
            get
            {
                return this.PictureUriField;
            }
            set
            {
                this.PictureUriField = value;
            }
        }
    }
}