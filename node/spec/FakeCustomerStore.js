///<reference path="../typings/bluebird/bluebird.d.ts"/>
'use strict';

function FakeCustomerStore(cs) {
  this.getCustomers = function () {
    return Promise.resolve(cs);
  };
  this.saveCustomer = function (customer) {
    var withAccoutnNr = cs.filter(function (c) {
      return c.accountNumber === c.accountNumber;
    });
    var c_0;
    if (withAccoutnNr.length > 0) {
      c_0 = withAccoutnNr[0];
      Object.getOwnPropertyNames(c_0).filter(function (name) {
        return name !== 'accountNumber';
      }).forEach(function (name) {
        c_0[name] = customer[name];
      });
      return Promise.resolve(true);
    }
    return Promise.resolve(false);
  };
}
module.exports = FakeCustomerStore;