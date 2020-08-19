using Demo.Api.Contracts.Models;
using Demo.Api.Mapper.EntityToModelMapper;
using Demo.API.Respository.Factory;
using System.Collections.Generic;

namespace Demo.API.Managers
{
    public class PatientManager: IPatientManager
    {
        public IRepositoryFactory RepositoryFactory { get; set; }

        public PatientManager(IRepositoryFactory repositoryFactory)
        {
            RepositoryFactory = repositoryFactory;
        }

        public List<Patient> GetAllPatient()
        {
            return EntityToModelMapper.MapTblPatientIntoPatient(RepositoryFactory.PatientRepository.GetAllPatient());
        }

        public Patient GetPatientById(int Id)
        {
            return EntityToModelMapper.MapTblPatientIntoPatient(RepositoryFactory.PatientRepository.GetPatientById(Id));
        }

        public void SavePatient(Patient patient)
        {
            RepositoryFactory.PatientRepository.SavePatient(EntityToModelMapper.MapTblPatientIntoPatient(patient));
        }
    }
}
