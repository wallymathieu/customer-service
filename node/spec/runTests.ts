///<reference path="../lib/config.ts"/>
///<reference path="CustomerServiceTest.ts"/>
///<reference path="../typings/requirejs/require.d.ts"/>
/// <amd-dependency path="../lib/CustomerService" />
'use strict';
//
requirejs.config({
    nodeRequire: require
});

require(['require', 'exports', 'spec/CustomerServiceTest'], () => {
    console.log('run!');
});
