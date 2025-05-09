using AutoMapper;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Domain.Services;

public class CRUDService<T,TDto,CreateAndUpdateTDto> : ICRUDService<TDto,CreateAndUpdateTDto> where T : class
{
    protected readonly ICRUDRepository<T> _repository;
    protected readonly IMapper _mapper;

    public CRUDService(ICRUDRepository<T> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TDto>> GetAllAsync(int pageNumber, int pageSize)
    {
        var (items, totalItemCount) = await _repository.GetAllAsync(pageNumber, pageSize);
        if (totalItemCount == 0)
        {
            throw new KeyNotFoundException($"{nameof(T)} with not data found.");
        }
        var pageData = new PageData(totalItemCount, pageSize, pageNumber);
        var dtos = _mapper.Map<IEnumerable<TDto>>(items);

        return new PaginatedList<TDto>(dtos.ToList(), pageData);
    }

    public async Task<TDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"{typeof(T).Name} with ID {id} not found.");
        }
        return _mapper.Map<TDto>(entity);
    }

    public async Task<TDto> CreateAsync(CreateAndUpdateTDto dto)
    {
        var entity = _mapper.Map<T>(dto);
        var createdEntity = await _repository.CreateAsync(entity);
        return _mapper.Map<TDto>(createdEntity);
    }
    

    public async Task<TDto> UpdateAsync(Guid id, CreateAndUpdateTDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"{typeof(T).Name} with ID {id} not found.");
        }
        _mapper.Map(dto, entity);
        var updatedEntity = await _repository.UpdateAsync(entity);
        return _mapper.Map<TDto>(updatedEntity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }
}