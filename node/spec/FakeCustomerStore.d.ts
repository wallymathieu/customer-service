///<reference path="../lib/CustomerStore.d.ts" />
///<reference path="../lib/Customer.d.ts" />

declare class FakeCustomerStore implements ICustomerStore{
	constructor(c:any);
	getCustomers(): Promise<Array<Customer>>;
}