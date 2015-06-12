///<reference path="../typings/xmlbuilder/xmlbuilder.d.ts"/>
///<reference path="../typings/lodash/lodash.d.ts"/>
"use strict";
var builder = require('xmlbuilder');

function CustomerService(customerStore) {
  function firstLetterToLowerCase(name) {
    return name[0].toLowerCase() + name.slice(1);
  }
  var properties = ['AccountNumber', 'AddressCity', 'AddressCountry', 'AddressStreet', 'FirstName', 'Gender', 'LastName', 'PictureUri'];
  this.getCustomers = function () {
    return customerStore.getCustomers().then(function (cs) {
      var arrayOfCustomers = builder.create('ArrayOfCustomer')
        .attribute("xmlns", "http://schemas.datacontract.org/2004/07/Customers")
        .attribute("xmlns:i", "http://www.w3.org/2001/XMLSchema-instance");

      cs.forEach(function (c) {
        var customer = arrayOfCustomers.element("Customer");
        properties.forEach(function (p) {
          var prop = customer.element(p);
          var value = c[firstLetterToLowerCase(p)];
          if (value === null || value === undefined) {
            prop.attribute("i:nil", "true");
          } else {
            prop.text(value);
          }
        });
      });
      return arrayOfCustomers;
    });
  };
}
module.exports = CustomerService;