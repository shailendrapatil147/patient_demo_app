using iMedOneDB.Models;
using System.Collections.Generic;

namespace Demo.API.Respository
{
    public interface IPatientRepository
    {
        void SavePatient(TBLPATIENT patient);
        List<TBLPATIENT> GetAllPatient();
        TBLPATIENT GetPatientById(int Id);
    }
}
