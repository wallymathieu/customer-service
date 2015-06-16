
class ArrayOfCustomer
  def initialize(customers)
    @customers = customers
  end
  
  def to_xml(options=nil)
    return @customers.to_xml(
      skip_types: true,
      root: 'ArrayOfCustomer'
      )
  end
  
end