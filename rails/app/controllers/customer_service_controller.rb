class CustomerServiceController < ApplicationController

  def get_all_customers
    array_of_customer = ArrayOfCustomer.new(Customer.all)
    respond_to do |format|
      format.json { render json: array_of_customer }
      format.xml { render xml: array_of_customer }
    end
  end

  def save_customer
    respond_to do |format|
        format.json { render json: {}, status: :unprocessable_entity }
        format.xml { render xml: {}, status: :unprocessable_entity }
    end
  end
end
