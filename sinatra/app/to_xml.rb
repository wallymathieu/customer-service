$properties = 'AccountNumber AddressCity AddressCountry AddressStreet FirstName Gender LastName PictureUri'.split(' ')

def uncapitalize val
    val[0, 1].downcase + val[1..-1]
end

def to_ruby_case val
    val.split(/([A-Z][a-z]*)/).reject(&:empty?).map( &method(:uncapitalize) ).join('_')
end

def serialize_property property, customer
  val = customer[to_ruby_case(property).to_sym]
  if not val.nil?
    return "  <#{property}>#{ val.to_s.encode(:xml => :text) }</#{property}>"
  else
    return "  <#{property} xsi:nil=\"true\"/>"
  end
end

def customer_from_hash customer
    $properties.map do |prop|
        [ to_ruby_case(prop), customer[prop] ]
    end.reduce({}) do |memo, nxt|
        memo[nxt[0].to_sym] = nxt[1]
        memo
    end
end

def to_customers_xml customers
    header = <<-END
<?xml version="1.0" encoding="utf-8"?>
<ArrayOfCustomer
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
    xmlns:xsd="http://www.w3.org/2001/XMLSchema"
    xmlns="http://schemas.datacontract.org/2004/07/Customers">
END
    serialized = customers.map do |customer|
        properties = $properties.map do |prop|
            serialize_property prop, customer
        end.join("\n")
        "<Customer>\n#{properties}\n</Customer>"
    end.join("\n")
    footer = "\n</ArrayOfCustomer>\n"
    return [header, serialized, footer].join("")
end

def to_success_xml c
    "<success>#{c}</success>"
end