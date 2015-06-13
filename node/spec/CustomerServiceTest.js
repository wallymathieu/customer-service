/*globals describe, beforeEach, it, expect*/
///<reference path='../typings/jasmine/jasmine.d.ts'/>
'use strict';
var CustomerService = require('../lib/CustomerService');
var FakeCustomerStore = require('./FakeCustomerStore');
var Customer = require('../lib/Customer');
var testHelper = require('./testHelper');
describe('CustomerService', function () {
  describe('getCustomers', function () {
    var service;
    var getAllCustomers;
    beforeEach(function () {
      var c = new Customer();
      c.firstName = 'Oskar';
      c.lastName = 'Gewalli';
      c.accountNumber = 0;
      c.gender = 'Male';
      service = new CustomerService(new FakeCustomerStore([c]));
      getAllCustomers = testHelper.getAllCustomersXml();
    });
    it('should return xml', function (done) {
      service.getCustomers().then(function (data) {
        expect(data.toString()).toEqual(getAllCustomers);
        done();
      });
    });
  });
  describe('saveCustomer', function () {
    var service, customer, c;
    beforeEach(function () {
      c = new Customer();
      c.firstName = 'Oskar';
      c.lastName = 'Gewalli';
      c.accountNumber = 1234;
      c.gender = 'Male';
      service = new CustomerService(new FakeCustomerStore([c]));
      customer = testHelper.getCustomerXml();
    });
    it('should return with updated with xml and update the customer', function (done) {
      service.saveCustomer(customer).then(function (data) {
        expect(data.toString()).toEqual(customer);
        expect(c.accountNumber).toEqual(0);
        done();
      });
    });
  });
});