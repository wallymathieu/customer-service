/*globals require, console*/
'use strict';
var http = require('http');
var CustomerService = require('./lib/CustomerService');
var CustomerStore = require('./lib/CustomerStore');
var cs = new CustomerService(new CustomerStore());
//var url = require('url');
http.createServer(function (req, res) {
  //console.log(Object.getOwnPropertyNames(req));
  //console.log(url.parse(req.url));
  switch (req.url) {
  case '/CustomerService/GetCustomers':
    res.writeHead(200, {
      'Content-Type': 'application/xml'
    });
    cs.getCustomers().then(function (customers) {
      res.end(customers.toString());
    });
    return;
  case '/CustomerService/GetAllCustomers':
    res.writeHead(200, {
      'Content-Type': 'application/xml'
    });
    cs.getCustomers().then(function (customers) {
      res.end(customers.toString());
    });
    return;
  case '/CustomerService/SaveCustomer':
    res.writeHead(200, {
      'Content-Type': 'application/xml'
    });
    res.end('<x>not implemented</x>');
    return;
  case '/':
    res.writeHead(200, {
      'Content-Type': 'text/html'
    });
    res.end('<html><body>' + '<h1>Customer Service</h1>' + '<a href="/CustomerService/GetAllCustomers">Customer Service GetAllCustomers</a>' + '</body></html>');
    return;
  default:
    res.writeHead(404, {
      'Content-Type': 'text/html'
    });
    res.end('<html><body>' +
      '<h1>Page not found</h1>' +
      '</body></html>');
    return;
  }
}).listen(3000);
console.log('Server running on port 3000.');