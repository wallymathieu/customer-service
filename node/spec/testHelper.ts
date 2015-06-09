
var fs = require("fs");
export = {
    getAllCustomersXmlSync: () => {
        return fs.readFileSync("GetAllCustomers.xml");
    }
};