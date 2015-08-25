require 'sinatra'
require_relative 'customer_service'
require_relative 'to_xml'
require 'nori'
dir = File.dirname(__FILE__)
if ENV['RACK_ENV'] == 'test' 
    cs = CustomerService.new
else
    cs = CustomerService.new
end
xml_parser = Nori.new(:strip_namespaces => true)
require 'rack/parser'
use Rack::Parser, :parsers => { 
    'application/json' => proc { |data| JSON.parse data },
    'application/xml'  => proc { |data| xml_parser.parse(data) }
}
get '/' do
  File.read(File.join(dir, 'public', 'index.html'))
end

get '/CustomerService.svc/GetAllCustomers' do
    content_type 'text/xml'
    to_customers_xml cs.get_all_customers
end
#require 'yaml'
post '/CustomerService.svc/SaveCustomer' do
    #puts params
    customer = customer_from_hash( params["Customer"] )
    content_type 'text/xml'
    to_success_xml cs.save_customer customer
end
