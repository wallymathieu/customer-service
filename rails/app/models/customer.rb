class Customer < ActiveRecord::Base
  attr_writer(:account_number,
      :address_city,
      :address_country,
      :address_street,
      :first_name,
      :gender,
      :last_name,
      :picture_uri)
end
