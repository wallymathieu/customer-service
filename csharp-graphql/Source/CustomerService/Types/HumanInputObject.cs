namespace CustomerService.Types
{
    using CustomerService.Models;
    using HotChocolate.Types;

    public class CustomerInputObject : InputObjectType<CustomerInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CustomerInput> descriptor)
        {
            descriptor
                .Name("CustomerInput")
                .Description("A customer.");

            descriptor
                .Field(x => x.Name)
                .Description("The name of the customer.");
            descriptor
                .Field(x => x.PictureUri)
                .Description("The customer picture.");
            descriptor
                .Field(x => x.Address)
                .Description("The address of the customer.");
        }
    }
}
