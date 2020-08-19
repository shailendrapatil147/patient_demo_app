using System.Collections.Generic;

namespace Demo.API.Respository
{
    public interface ILocationRepository
    {
        List<iMedOneDB.Models.Tblcity> GetAllCities();
        List<iMedOneDB.Models.Tblstate> GetAllStates();
    }
}
