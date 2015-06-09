///<reference path="../typings/bluebird/bluebird.d.ts"/>
///<reference path="Customer.d.ts"/>

interface ICustomerStore {
    getCustomers(): Promise<Array<Customer>>;
}
