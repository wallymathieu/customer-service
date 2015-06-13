///<reference path='../typings/xmlbuilder/xmlbuilder.d.ts'/>
///<reference path='../typings/lodash/lodash.d.ts'/>
'use strict';
var builder = require('xmlbuilder');
//var _ = require('lodash');

function CustomerService(customerStore) {
  function firstLetterToLowerCase(name) {
    return name[0].toLowerCase() + name.slice(1);
  }
  var properties = ['AccountNumber', 'AddressCity', 'AddressCountry', 'AddressStreet', 'FirstName', 'Gender', 'LastName', 'PictureUri'];
  this.getCustomers = function () {

    function serializeProperty(customerXml, property, customer) {
      var prop = customerXml.element(property);
      var value = customer[firstLetterToLowerCase(property)];
      if (value === null || value === undefined) {
        prop.attribute('i:nil', 'true');
      } else {
        prop.text(value);
      }
    }

    function serializeCustomer(arrayOfCustomers, customer) {
      var customerXml = arrayOfCustomers.element('Customer');
      properties.forEach(function (property) {
        serializeProperty(customerXml, property, customer);
      });
    }

    return customerStore.getCustomers().then(function (cs) {
      var arrayOfCustomers = builder.create('ArrayOfCustomer')
        .attribute('xmlns', 'http://schemas.datacontract.org/2004/07/Customers')
        .attribute('xmlns:i', 'http://www.w3.org/2001/XMLSchema-instance');

      cs.forEach(function (customer) {
        serializeCustomer(arrayOfCustomers, customer);
      });
      return arrayOfCustomers;
    });
  };
}
module.exports = CustomerService;