///<reference path='../typings/lodash/lodash.d.ts'/>
'use strict';
var _ = require('lodash');

function Customer() {
  var properties = ['accountNumber', 'addressCity', 'addressCountry', 'addressStreet', 'firstName', 'gender', 'lastName', 'pictureUri'];
  var def = _.reduce(properties, function (memo, next) {
    memo[next] = {
      value: null,
      writable: true
    };
    return memo;
  }, {});
  Object.defineProperties(this, def);
  Object.seal(this);
}
module.exports = Customer;