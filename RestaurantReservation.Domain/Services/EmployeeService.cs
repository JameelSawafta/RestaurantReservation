using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Employee;

namespace RestaurantReservation.Domain.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<EmployeeDto>> GetAllEmployeesAsync(int pageNumber, int pageSize, string baseUrl)
    {
        var (employees, totalItemCount) = await _employeeRepository.GetAllAsync(pageNumber, pageSize);

        string GeneratePageLink(int page) => $"{baseUrl}?pageNumber={page}&pageSize={pageSize}";

        var pageData = new PageData(totalItemCount, pageSize, pageNumber, GeneratePageLink);
        var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

        return new PaginatedList<EmployeeDto>(employeeDtos.ToList(), pageData);
    }

    public async Task<EmployeeDto> GetEmployeeByIdAsync(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto employeeDto)
    {
        var employee = _mapper.Map<Employee>(employeeDto);
        var createdEmployee = await _employeeRepository.CreateAsync(employee);
        return _mapper.Map<EmployeeDto>(createdEmployee);
    }

    public async Task<EmployeeDto> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto employeeDto)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null) return null;

        _mapper.Map(employeeDto, employee);
        var updatedEmployee = await _employeeRepository.UpdateAsync(employee);
        return _mapper.Map<EmployeeDto>(updatedEmployee);
    }

    public async Task<bool> DeleteEmployeeAsync(Guid id)
    {
        return await _employeeRepository.DeleteAsync(id);
    }
    
    public async Task<PaginatedList<EmployeeDto>> GetManagersAsync(int pageNumber, int pageSize, string baseUrl)
    {
        var (managers, totalItemCount) = await _employeeRepository.GetManagersAsync(pageNumber, pageSize);

        string GeneratePageLink(int page) => $"{baseUrl}?pageNumber={page}&pageSize={pageSize}";

        var pageData = new PageData(totalItemCount, pageSize, pageNumber, GeneratePageLink);
        var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(managers);

        return new PaginatedList<EmployeeDto>(employeeDtos.ToList(), pageData);
    }
}