class Customer < ActiveRecord::Base
  attr_writer(:account_number,
      :address_city,
      :address_country,
      :address_street,
      :first_name,
      :gender,
      :last_name,
      :picture_uri)
  def to_xml(options = {})
    options[:indent] ||= 2
    options[:root] = nil
    options[:skip_instruct] = true
    options[:camelize] = true 
    options[:skip_types] = true
    options[:except] = ['created_at', 'updated_at', 'id']
    Serializer.new(self, options).serialize()
  end
end
