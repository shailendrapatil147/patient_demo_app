using Demo.Api.Contracts.Models;
using Demo.Api.Mapper.EntityToModelMapper;
using Demo.API.Respository.Factory;
using System.Collections.Generic;
using System.Linq;

namespace Demo.API.Managers
{
    public class LocationManager : ILocationManager
    {
        public IRepositoryFactory RepositoryFactory { get; set; }

        public LocationManager(IRepositoryFactory repositoryFactory)
        {
            RepositoryFactory = repositoryFactory;
        }

        public List<City> GetAllCities()
        {
            return EntityToModelMapper.MapTblCityIntoCity(RepositoryFactory.LocationRepository.GetAllCities());
        }

        public List<State> GetAllStates()
        {
            return EntityToModelMapper.MapTblStateIntoState(RepositoryFactory.LocationRepository.GetAllStates());
        }
    }
}
