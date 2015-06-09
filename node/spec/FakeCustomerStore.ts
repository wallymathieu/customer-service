///<reference path="../typings/bluebird/bluebird.d.ts"/>

function FakeCustomerStore(c) {
	this.getCustomers = ()=>{
		return Promise.resolve(c);
	}; 
}

export = FakeCustomerStore;