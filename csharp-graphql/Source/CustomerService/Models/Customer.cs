using System;

namespace CustomerService.Models
{

    public record Customer(int AccountNumber, Name Name, CustomerGender Gender, Address Address, Uri? PictureUri,
        DateTimeOffset Added)
    {
    }

    public record Name(string First, string Last)
    {

    }
    public record Address(string City, string Country, string Street)
    {
        public static Address Empty => new("", "", "");
    }
    public record CustomerInput(Name Name, CustomerGender Gender, Address Address, Uri? PictureUri)
    {
    }
}
