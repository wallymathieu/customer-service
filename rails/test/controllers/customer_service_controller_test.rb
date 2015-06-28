require 'test_helper'

$get_all_customers = <<-END
<?xml version="1.0" encoding="utf-8"?>
<ArrayOfCustomer xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="http://schemas.datacontract.org/2004/07/Customers">
  <Customer>
    <AccountNumber>1</AccountNumber>
    <AddressCity nil="true"/>
    <AddressCountry nil="true"/>
    <AddressStreet nil="true"/>
    <FirstName>Oskar</FirstName>
    <Gender>Male</Gender>
    <LastName>Gewalli</LastName>
    <PictureUri nil="true"/>
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
    puts response.body
    assert_equal( $get_all_customers, response.body )
    #assert_not_nil assigns(:array_of_customers)
  end

  test "should be able to save customer" do
    post :save_customer, $single_customer, :format => "xml"
    assert_response :success
    # response.body
    #assert_not_nil assigns(:success)
  end

end
