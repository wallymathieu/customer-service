///<reference path="../typings/bluebird/bluebird.d.ts"/>

declare class CustomerService {
    constructor();
    getCustomers(): Promise<Object>;
}
