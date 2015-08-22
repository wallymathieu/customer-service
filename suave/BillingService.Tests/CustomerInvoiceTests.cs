using NUnit.Framework;
using System;
using Customers;
using Microsoft.FSharp.Core;
using OptionalUrl = Microsoft.FSharp.Core.FSharpOption<Customers.Url>;
namespace BillingService.Tests
{
	[TestFixture]
	public class CustomerInvoiceTests
	{
		[Test]
		public void Can_generate_report ()
		{
			var customer = new Customer(1, 
				firstName: "First name", 
				lastName: "Last name", 
				addressCity: "", addressCountry:"", addressStreet:"",
				gender: CustomerGender.Female,
                pictureUri: OptionalUrl.Some(new Url("http://someurl.com/something?some=asd"))
			);

			var generate = new GenerateCustomerInvoice ();
			generate.Create (customer, new []{ "1" });
		}

		[Test]
		public void Can_generate_report_when_uri_is_not_set ()
		{
			var customer = new Customer(1, 
				firstName: "First name", 
				lastName: "Last name", 
				addressCity: "", addressCountry:"", addressStreet:"",
				gender: CustomerGender.Female,
                pictureUri: OptionalUrl.None
			);

			var generate = new GenerateCustomerInvoice ();
			generate.Create (customer, new []{ "1" });
		}
	}
}

