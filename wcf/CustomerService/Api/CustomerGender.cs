using System.Runtime.Serialization;
namespace Customers
{

    [DataContract(Name = "CustomerGender", Namespace = "http://schemas.datacontract.org/2004/07/Customers")]
    public enum CustomerGender : int
    {
        [EnumMember()] Male = 0,
        [EnumMember()] Female = 1,
        [EnumMember()] Boy = 2,
        [EnumMember()] Girl = 3,
    }
}