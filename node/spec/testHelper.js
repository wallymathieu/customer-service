"use strict";
var fs = require("fs");
var path = require("path");
module.exports = {
  getAllCustomersXmlSync: function () {
    return '<ArrayOfCustomer' +
      ' xmlns="http://schemas.datacontract.org/2004/07/Customers"' +
      ' xmlns:i="http://www.w3.org/2001/XMLSchema-instance">' +
      '<Customer>' +
      '<AccountNumber>0</AccountNumber>' +
      '<AddressCity i:nil="true"/>' +
      '<AddressCountry i:nil="true"/>' +
      '<AddressStreet i:nil="true"/>' +
      '<FirstName>Oskar</FirstName>' +
      '<Gender>Male</Gender>' +
      '<LastName>Gewalli</LastName>' +
      '<PictureUri i:nil="true"/>' +
      '</Customer>' +
      '</ArrayOfCustomer>';
  }
};