Rails.application.routes.draw do
  # For details on the DSL available within this file, see http://guides.rubyonrails.org/routing.html
  get 'CustomerService.svc/GetAllCustomers', controller: 'customer_service', action: 'get_all_customers'
  post 'CustomerService.svc/SaveCustomer', controller: 'customer_service', action: 'save_customer'

end
