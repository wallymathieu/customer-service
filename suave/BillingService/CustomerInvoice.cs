using System;
using System.Text;
using Customers;
using Microsoft.FSharp.Core;
namespace BillingService
{
	public class GenerateCustomerInvoice
	{
		public string Create(Customer customer, string[] products)
		{
			var sb = new StringBuilder ();
			sb.AppendLine(string.Format("Invoice for #{0}", customer.AccountNumber));
			sb.AppendLine();
			sb.AppendLine(string.Format("To:\t\t{0} {1}", customer.FirstName, customer.LastName));
			sb.AppendLine(string.Format("Street:\t\t{0}",  customer.AddressStreet));
			sb.AppendLine(string.Format("City:\t\t{0}",    customer.AddressCity));
			sb.AppendLine(string.Format("Country:\t\t{0}", customer.AddressCountry));
			if (customer.PictureUri != null)
			{
				sb.AppendLine(string.Format("Picture:\t\t{0}", customer.PictureUri.Value));
			}
			return sb.ToString();
		}
	}
}

