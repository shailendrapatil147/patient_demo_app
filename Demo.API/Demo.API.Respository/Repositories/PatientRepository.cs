using iMedOneDB;
using iMedOneDB.Models;
using System.Collections.Generic;
using System.Linq;

namespace Demo.API.Respository.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        public List<TBLPATIENT> GetAllPatient()
        {
            return DBContext.GetData<TBLPATIENT>().ToList();
        }

        public TBLPATIENT GetPatientById(int Id)
        {
            return DBContext.GetData<TBLPATIENT>(Id).FirstOrDefault();
        }

        public void SavePatient(TBLPATIENT patient)
        {
            DBContext.SaveAll(new List<TBLPATIENT>{patient});
        }
    }
}
