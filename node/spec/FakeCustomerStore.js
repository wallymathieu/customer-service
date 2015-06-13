///<reference path="../typings/bluebird/bluebird.d.ts"/>
"use strict";

function FakeCustomerStore(c) {
  this.getCustomers = function () {
    return Promise.resolve(c);
  };
  this.saveCustomer = function (customer) {
    return Promise.resolve(c);
  };
}
module.exports = FakeCustomerStore;