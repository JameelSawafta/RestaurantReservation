using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models.Customer;

namespace RestaurantReservation.Domain.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }

    public async Task<CustomerDto> GetCustomerByIdAsync(Guid id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        var createdCustomer = await _customerRepository.CreateAsync(customer);
        return _mapper.Map<CustomerDto>(createdCustomer);
    }

    public async Task<CustomerDto> UpdateCustomerAsync(Guid id, UpdateCustomerDto customerDto)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null) return null;

        _mapper.Map(customerDto, customer); // Update existing customer with new values
        var updatedCustomer = await _customerRepository.UpdateAsync(customer);
        return _mapper.Map<CustomerDto>(updatedCustomer);
    }

    public async Task<bool> DeleteCustomerAsync(Guid id)
    {
        return await _customerRepository.DeleteAsync(id);
    }
}