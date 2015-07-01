require 'test_helper'

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

class CustomerServiceControllerTest < ActionController::TestCase

  test "should get all customer" do
    get :get_all_customers, :format => "xml"
    assert_response :success
    assert_equal( $get_all_customers, response.body )
  end

  test "should be able to save customer" do
    customer = {
      "xmlns:xsi"=>"http://www.w3.org/2001/XMLSchema-instance", 
      "xmlns:xsd"=>"http://www.w3.org/2001/XMLSchema", 
      "xmlns"=>"http://schemas.datacontract.org/2004/07/Customers", 
      "AccountNumber"=>"1", 
      "AddressCity"=>"Luleaa", 
      "AddressCountry"=>"Sweden", 
      "AddressStreet"=>{"xsi:nil"=>"true"}, 
      "FirstName"=>"Oskar", 
      "Gender"=>"Male", 
      "LastName"=>"Gewalli", 
      "PictureUri"=>{"xsi:nil"=>"true"}
    }
    post :save_customer, $single_customer, { :Customer => customer,
      'CONTENT_TYPE' => 'application/xml', 
      :format => "xml"
    }
    assert_response :success
    oskar = Customer.find_by(account_number: 1)
    assert_equal( 'Luleaa', oskar.address_city )
    assert_equal( 'Sweden', oskar.address_country )
    assert_equal( nil, oskar.address_street )
  end
end
