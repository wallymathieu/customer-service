///<reference path="../typings/jasmine/jasmine.d.ts"/>
///<reference path="../lib/CustomerService.d.ts" />
///<reference path="../lib/Customer.d.ts" />
///<reference path="FakeCustomerStore.d.ts" />
///<reference path="testHelper.ts" />
/// <amd-dependency path="../lib/CustomerService" />
/// <amd-dependency path="../lib/Customer" />
/// <amd-dependency path="./FakeCustomerStore" />
/// <amd-dependency path="./testHelper" />
'use strict';
import CustomerService = require("../lib/CustomerService");
import FakeCustomerStore = require("./FakeCustomerStore");
var Customer = require("../lib/Customer");
var testHelper = require("./testHelper");

describe('CustomerService', function() {
  describe('getCustomers', function() {
    var service;
    var getAllCustomers;
    beforeEach(function() {
      var c = new Customer();
      service = new CustomerService(new FakeCustomerStore([c]));
      getAllCustomers = testHelper.getAllCustomersXmlSync();
    });
    it('should return xml', function(done) {

      service.getCustomers().then(function(data) {
        expect(data).toEqual('<>');
        done();
      });
    });
  });
});

