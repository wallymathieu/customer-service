class CustomerServiceController < ApplicationController

  def get_all_customers
    respond_to do |format|
      format.json { render json: fake_customers }
      format.xml { render xml: fake_customers }
    end
  end

  def save_customer
    respond_to do |format|
        format.json { render json: {}, status: :unprocessable_entity }
        format.xml { render xml: {}, status: :unprocessable_entity }
    end
  end

  private
  def fake_customers
    c = Customer.new
    c.first_name = "Oskar"
    c.last_name = "Gewalli"
    return [c]
  end
end
