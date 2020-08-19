using iMedOneDB;
using System.Collections.Generic;
using System.Linq;

namespace Demo.API.Respository.Repositories
{
    public class LocationRepository: ILocationRepository
    {
        public List<iMedOneDB.Models.Tblcity> GetAllCities()
        {
            return DBContext.GetData<iMedOneDB.Models.Tblcity>().ToList();
        }

        public List<iMedOneDB.Models.Tblstate> GetAllStates()
        {
            return DBContext.GetData<iMedOneDB.Models.Tblstate>().ToList();
        }
    }
}
