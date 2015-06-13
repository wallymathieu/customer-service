'use strict';
var Customer = require('./Customer');

function CustomerStore() {
  this.getCustomers = function () {
    var c = new Customer();
    c.firstName = 'Oskar';
    c.lastName = 'Gewalli';
    c.accountNumber = 0;
    c.gender = 'Male';
    return Promise.resolve([c]);
  };
}
module.exports = CustomerStore;