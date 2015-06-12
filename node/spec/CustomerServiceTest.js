///<reference path="../typings/jasmine/jasmine.d.ts"/>
///<reference path="../lib/CustomerService.d.ts" />
///<reference path="../lib/Customer.d.ts" />
///<reference path="FakeCustomerStore.d.ts" />
///<reference path="testHelper.ts" />
'use strict';
var CustomerService = require("../lib/CustomerService");
var FakeCustomerStore = require("./FakeCustomerStore");
var Customer = require("../lib/Customer");
var testHelper = require("./testHelper");
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
      getAllCustomers = testHelper.getAllCustomersXmlSync();
    });
    it('should return xml', function (done) {
      service.getCustomers().then(function (data) {
        expect(data.toString()).toEqual(getAllCustomers);
        done();
      });
    });
  });
});