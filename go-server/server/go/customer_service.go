package customerservice

type CustomerService interface {
	GetAll() []Customer
	Add(c Customer) error
}

type InMemoryCustomerService ArrayOfCustomer

func (a *InMemoryCustomerService) GetAll() []Customer {
	return a.Customer
}

func (a *InMemoryCustomerService) Add(c Customer) error {
	a.Customer = append(a.Customer, c)
	return nil
}
