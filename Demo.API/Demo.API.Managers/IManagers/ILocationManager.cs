using Demo.Api.Contracts.Models;
using System.Collections.Generic;

namespace Demo.API.Managers
{
    public interface ILocationManager
    {
        List<City> GetAllCities();
        List<State> GetAllStates();
    }
}
