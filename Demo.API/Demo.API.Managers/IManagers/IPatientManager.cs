using Demo.Api.Contracts.Models;
using System.Collections.Generic;

namespace Demo.API.Managers
{
    public interface IPatientManager
    {
        void SavePatient(Patient patient);
        List<Patient> GetAllPatient();
        Patient GetPatientById(int Id);
    }
}
