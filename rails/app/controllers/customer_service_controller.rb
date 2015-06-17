class CustomerServiceController < ApplicationController

  def get_all_customers
    @array_of_customer = Customer.all
    respond_to do |format|
      format.json { render json: @array_of_customer }
      format.xml 
    end
  end

  def save_customer
    respond_to do |format|
        format.json { render json: {}, status: :unprocessable_entity }
        format.xml { render xml: {}, status: :unprocessable_entity }
    end
  end
end
