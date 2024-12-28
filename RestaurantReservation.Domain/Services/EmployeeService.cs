using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Employee;

namespace RestaurantReservation.Domain.Services;

public class EmployeeService : CRUDService<Employee,EmployeeDto,CreateAndUpdateEmployeeDto>, IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    
    public EmployeeService(ICRUDRepository<Employee> repository, IMapper mapper, IEmployeeRepository employeeRepository) : base(repository, mapper)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<PaginatedList<EmployeeDto>> GetManagersAsync(int pageNumber, int pageSize)
    {
        var (managers, totalItemCount) = await _employeeRepository.GetManagersAsync(pageNumber, pageSize);
        if (totalItemCount == 0)
        {
            throw new KeyNotFoundException($"{nameof(Employee)} with not data found.");
        }

        var pageData = new PageData(totalItemCount, pageSize, pageNumber);
        var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(managers);

        return new PaginatedList<EmployeeDto>(employeeDtos.ToList(), pageData);
    }
    
    public async Task<decimal> GetAverageOrderAmountByEmployeeAsync(Guid employeeId)
    {
        var averageOrderAmount = await _employeeRepository.GetAverageOrderAmountByEmployeeAsync(employeeId);
        if (averageOrderAmount == 0)
            throw new KeyNotFoundException($"No orders found for employee with ID {employeeId}.");
        return averageOrderAmount;
    }
}