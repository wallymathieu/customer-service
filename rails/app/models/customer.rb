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
    #include ActiveModel::Serializers::Xml
    options[:root] = nil
    options[:camelize] = true 
    options[:skip_types] = true
    options[:except] = ['created_at', 'updated_at', 'id']
    xml = options[:builder] ||= ::Builder::XmlMarkup.new(indent: options[:indent])
    Serializer.new(self, options).serialize()
    #XmlSerializer.new(self, options).serialize(&block)
  end
end
