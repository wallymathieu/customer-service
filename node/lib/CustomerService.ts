///<reference path="xmlbuilder.d.ts"/>
///<reference path="Customer.ts"/>
///<reference path="CustomerStore.ts"/>
///<reference path="./CustomerStore.d.ts"/>

/// <amd-dependency path="xmlbuilder" />

import builder = require('xmlbuilder');

function CustomerService(customerStore:ICustomerStore) {
    this.getCustomers = () => { 
        
    };
}

export = CustomerService;