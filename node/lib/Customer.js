///<reference path="../typings/lodash/lodash.d.ts"/>
var _ = require('lodash');
"use strict";

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