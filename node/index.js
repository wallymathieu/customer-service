/*globals require, console*/
'use strict';
var CustomerService = require('./lib/CustomerService');
var CustomerStore = require('./lib/CustomerStore');
var cs = new CustomerService(new CustomerStore());
var express = require('express');
var app = express();

app.get('/', function (req, res) {
  res.send('<html><body>' +
    '<h1>Customer Service</h1>' +
    '<a href="/CustomerService/GetAllCustomers">Customer Service GetAllCustomers</a>' +
    '</body></html>');
});

app.get('/CustomerService/GetCustomers', function (req, res) {
  res.header('Content-Type', 'application/xml');
  cs.getCustomers().then(function (customers) {
    res.send(customers.toString());
  });
});

app.get('/CustomerService/GetAllCustomers', function (req, res) {
  res.header('Content-Type', 'application/xml');
  cs.getCustomers().then(function (customers) {
    res.send(customers.toString());
  });
});

app.post('/CustomerService/SaveCustomer', function (req, res) {
  res.header('Content-Type', 'application/xml');
  res.send('<x>not implemented</x>');
});

app.get('*', function (req, res) {
  res.send('Page not found', 404);
});

var server = app.listen(3000, function () {

  var host = server.address().address;
  var port = server.address().port;

  console.log('Listening at http://%s:%s', host, port);

});