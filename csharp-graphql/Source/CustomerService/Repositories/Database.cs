namespace CustomerService.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CustomerService.Models;

    public static class Database
    {
        static Database()
        {
            var created = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);

            IReadOnlyList<string> splitIntoColumns(string line) =>
                (from col in line.Split(new[] {','})
                    select col.Trim()).ToList();

            Customer mapToCustomer(IReadOnlyList<string> columns) =>
                new Customer(
                    Address:Address.Empty,
                    AccountNumber : Int32.Parse(s: columns.First()),
                    Name: new Name(First: columns[index: 1],Last: columns[index: 2]),
                    Gender:CustomerGender.Male,
                    Added:created,
                    PictureUri:null);

            var cs = from line in @"1,Oskar,Gewalli,
            2,Greta,Skogsberg,
            3,Matthias,Wallisson,
            4,Ada,Lundborg,
            5,Daniel,Örnvik,
            6,Johan,Irisson,
            7,Edda,Tyvinge".Split(separator: new[] {'\n', '\r'}, options: System.StringSplitOptions.RemoveEmptyEntries)
                    let columns = splitIntoColumns(line)
                    select mapToCustomer(columns)
                ;

            Customers = cs.ToList();
        }

        public static List<Customer> Customers { get; }
    }
}
