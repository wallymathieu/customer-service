namespace CustomerService.Types
{
    using CustomerService.Models;

    public class AddressObject : ObjectType<Address>
    {
        protected override void Configure(IObjectTypeDescriptor<Address> descriptor)
        {
            descriptor
                .Name("Address")
                .Description("An address.");

            descriptor
                .Field(x => x.City)
                .Description("The city.");
            descriptor
                .Field(x => x.Country)
                .Description("The country.");
            descriptor
                .Field(x => x.Street)
                .Description("The street.");

        }
    }
}
