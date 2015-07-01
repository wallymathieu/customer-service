class CustomerServiceController < ApplicationController
  skip_before_filter :verify_authenticity_token

  def get_all_customers
    @array_of_customer = Customer.all
    respond_to do |format|
      format.json { render json: @array_of_customer }
      format.xml 
    end
  end

  def save_customer 
    customer = params[:Customer]
    hash = CustomerServiceHelper::xml_hash_to_ruby_convention(customer)
    with_account_number = Customer.find_by(account_number: hash['account_number'])
    if !with_account_number
      respond_to do |format|
          format.json { render json: false, status: :unprocessable_entity }
          format.xml { render xml: false, status: :unprocessable_entity }
      end
    else
      with_account_number.accept_changes(hash)
      with_account_number.save
      respond_to do |format|
          format.json { render json: true }
          format.xml { render xml: true }
      end
    end
  end
end
