namespace CustomerService.Types
{
    using CustomerService.Models;

    public class NameObject : ObjectType<Name>
    {
        protected override void Configure(IObjectTypeDescriptor<Name> descriptor)
        {
            descriptor
                .Name("Name")
                .Description("A name.");
            descriptor
                .Field(x => x.First)
                .Description("The first name.");

            descriptor
                .Field(x => x.Last)
                .Description("The last name.");

        }
    }
}
