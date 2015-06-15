///<reference path='../typings/xmlbuilder/xmlbuilder.d.ts'/>
///<reference path='../typings/lodash/lodash.d.ts'/>
///<reference path='../typings/bluebird/bluebird.d.ts'/>
'use strict';
var builder = require('xmlbuilder');
var xml2js = require('xml2js');
//var Promise = require('bluebird');
var _ = require('lodash');
var properties = ['AccountNumber', 'AddressCity', 'AddressCountry', 'AddressStreet', 'FirstName', 'Gender', 'LastName', 'PictureUri'];

function firstLetterToLowerCase(name) {
  return name[0].toLowerCase() + name.slice(1);
}

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

function parseCustomerXml(customerXml) {
  var parser = new xml2js.Parser();
  return new Promise(function (resolve) {
    parser.addListener('end', function (result) {
      resolve(result);
    });
    parser.parseString(customerXml);
  });
}

function isNil(v) {
  return _.isObject(v) && v.$ && v.$['i:nil'] === 'true';
}

function getValue(v) {
  if (_.isArray(v)) {
    if (v.length > 0) {
      if (isNil(v[0])) {
        return null;
      }
      return v[0];
    }
    return null;
  }
}

function CustomerService(customerStore) {
  this.getCustomers = function () {
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

  this.saveCustomer = function (customerXml) {
    return parseCustomerXml(customerXml).then(function (xml) {
      var c = xml.Customer;
      return _.reduce(Object.getOwnPropertyNames(c), function (memo, next) {
        var p = firstLetterToLowerCase(next);
        memo[p] = getValue(c[next]);
        return memo;
      }, {});
    }).then(function (parsed) {
      return customerStore.saveCustomer(parsed).then(function (c) {
        return builder.create('boolean').text(c);
      });
    });
  };
}

module.exports = CustomerService;