///<reference path='../typings/lodash/lodash.d.ts'/>
'use strict';
var _ = require('lodash');

function defineWritableProperties(instance, properties) {
  Object.defineProperties(instance, _.reduce(properties, function (memo, next) {
    memo[next] = {
      value: null,
      writable: true
    };
    return memo;
  }, {}));
}

function Customer() {
  var properties = [
    'accountNumber',
    'addressCity',
    'addressCountry',
    'addressStreet',
    'firstName',
    'gender',
    'lastName',
    'pictureUri'];
  defineWritableProperties(this, properties);
  Object.seal(this);
}
module.exports = Customer;