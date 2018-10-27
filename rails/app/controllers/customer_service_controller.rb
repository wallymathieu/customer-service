class CustomerServiceController < ApplicationController
  skip_before_action :verify_authenticity_token, raise: false

  def get_all_customers
    @array_of_customer = Customer.all
    respond_to do |format|
      format.json { render json: @array_of_customer }
      format.xml
    end
  end

  def save_customer 
    customer = customer_params
    hash = CustomerServiceHelper::xml_hash_to_ruby_convention(customer.to_h)
    with_account_number = Customer.find_by(account_number: hash['account_number'])
    if with_account_number.nil?
      customer = Customer.new()
      customer.accept_changes(hash)
      customer.save
      msg = "customer updated"
      respond_to do |format|
          format.json { render json: [msg] }
          format.xml { render xml: [msg] }
      end
    else
      with_account_number.accept_changes(hash)
      with_account_number.save
      msg = "customer saved"
      respond_to do |format|
          format.json { render json: [msg] }
          format.xml { render xml: [msg] }
      end
    end
  end
  private
  def customer_params
    params.require(:Customer).permit([
      "xmlns:xsi", "xmlns:xsd", "xmlns",
      "AccountNumber", "AddressCity", "AddressCountry", "AddressStreet",
      "FirstName", "LastName", "Gender", "PictureUri"
    ])
  end
end
