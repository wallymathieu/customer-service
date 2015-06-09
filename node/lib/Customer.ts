
function Customer() {
	Object.defineProperties(this, {
		firstName:{
			get:function () {
				return '';
			}
		},
		lastName:{
			get:function () {
				return '';
			}
		}
	});
}

export = Customer;