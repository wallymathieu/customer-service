namespace CustomerService.Types
{
    using CustomerService.Constants;
    using CustomerService.Models;
    using CustomerService.Resolvers;
    using HotChocolate.Types;

    public class CustomerObject : ObjectType<Customer>
    {
        protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
        {
            descriptor
                .Name("Customer")
                .Description("A customer.");

            // descriptor.Authorize(AuthorizationPolicyName.Admin); // To require authorization for all fields in this type.
            descriptor
                .ImplementsNode()
                .IdField(x => x.AccountNumber)
                .ResolveNodeWith<CustomerResolver>(x => x.GetCustomerAsync(default!, default!, default!));
            descriptor
                .Field(x => x.Gender)
                .Description("The customer gender.");
            descriptor
                .Field(x => x.Name)
                .Description("The customer name.");
            descriptor
                .Field(x => x.Address)
                .Description("The customer address.");
            descriptor
                .Field(x => x.PictureUri)
                .Description("The customer picture.");

        }
    }
}
