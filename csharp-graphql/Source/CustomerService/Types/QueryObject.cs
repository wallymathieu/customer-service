namespace CustomerService.Types
{
    using CustomerService.Resolvers;
    using HotChocolate.Types;

    /// <summary>
    /// All queries defined in the schema used to retrieve data.
    /// </summary>
    public class QueryObject : ObjectType<QueryResolver>
    {
        protected override void Configure(IObjectTypeDescriptor<QueryResolver> descriptor)
        {
            descriptor
                .Name("Query")
                .Description("The query type, represents all of the entry points into our object graph.");

            descriptor
                .Field(x => x.GetCustomersAsync(default!, default!))
                .Description("Gets customers.")
                .UsePaging()
                .UseFiltering()
                .UseSorting();
            descriptor
                .Field(x => x.GetCustomersByIdsAsync(default!, default!, default!))
                .Description("Gets customers by one or more unique identifiers.")
                .UsePaging()
                .UseFiltering()
                .UseSorting();
        }
    }
}
