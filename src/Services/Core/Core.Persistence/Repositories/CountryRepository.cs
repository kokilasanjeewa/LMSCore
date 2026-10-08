using Core.Domain.Entities;
using Core.Application.Interfaces.Repositories;

namespace Core.Persistence.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IGenericRepository<Country> _repository;

        public CountryRepository(IGenericRepository<Country> repository) 
        {
            _repository = repository;
        } 
    }
}
