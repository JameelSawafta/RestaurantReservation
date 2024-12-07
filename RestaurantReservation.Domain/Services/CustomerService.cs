using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
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

    public async Task<PaginatedList<CustomerDto>> GetAllCustomersAsync(int pageNumber, int pageSize, string baseUrl)
    {
        var (customers, totalItemCount) = await _customerRepository.GetAllAsync(pageNumber, pageSize);
        
        string GeneratePageLink(int page) => $"{baseUrl}?pageNumber={page}&pageSize={pageSize}";

        var pageData = new PageData(totalItemCount, pageSize, pageNumber, GeneratePageLink);
        
        var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);

        return new PaginatedList<CustomerDto>(customerDtos.ToList(), pageData);
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

        _mapper.Map(customerDto, customer);
        var updatedCustomer = await _customerRepository.UpdateAsync(customer);
        return _mapper.Map<CustomerDto>(updatedCustomer);
    }

    public async Task<bool> DeleteCustomerAsync(Guid id)
    {
        return await _customerRepository.DeleteAsync(id);
    }
}