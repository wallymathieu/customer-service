require_relative 'test_helper'

$get_all_customers = <<-END
<?xml version="1.0" encoding="utf-8"?>
<ArrayOfCustomer
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
    xmlns:xsd="http://www.w3.org/2001/XMLSchema"
    xmlns="http://schemas.datacontract.org/2004/07/Customers">
<Customer>
  <AccountNumber>1</AccountNumber>
  <AddressCity xsi:nil="true"/>
  <AddressCountry xsi:nil="true"/>
  <AddressStreet xsi:nil="true"/>
  <FirstName>Oskar</FirstName>
  <Gender>Male</Gender>
  <LastName>Gewalli</LastName>
  <PictureUri xsi:nil="true"/>
</Customer>
</ArrayOfCustomer>
END

$single_customer = <<-END
<?xml version="1.0" encoding="utf-8"?>
<Customer xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://schemas.datacontract.org/2004/07/Customers">
  <AccountNumber>1</AccountNumber>
  <AddressCity xsi:nil="true" />
  <AddressCountry xsi:nil="true" />
  <AddressStreet xsi:nil="true" />
  <FirstName>Oskar</FirstName>
  <Gender>Male</Gender>
  <LastName>Gewalli</LastName>
  <PictureUri xsi:nil="true" />
</Customer>
END

class CustomerServiceTest < Minitest::Test
  include Rack::Test::Methods

  def app
    Sinatra::Application
  end

  def test_it_can_serve_up_index
    get '/'
    assert last_response.ok?
    assert_equal "text/html;charset=utf-8", last_response["Content-Type"]
  end

  def test_should_get_all_customer
    get '/CustomerService.svc/GetAllCustomers'
    assert last_response.ok?
    assert_equal "text/xml;charset=utf-8", last_response["Content-Type"]
    assert_equal $get_all_customers, last_response.body
  end

  def test_should_be_able_to_save_customer
    post '/CustomerService.svc/SaveCustomer', $single_customer, { 
        'CONTENT_TYPE' => 'application/xml', 
        'Content-Type' => 'application/xml'
      }
    assert last_response.ok?
    assert_equal "text/xml;charset=utf-8", last_response["Content-Type"]
    assert_equal "<success>true</success>", last_response.body 

  end
end
