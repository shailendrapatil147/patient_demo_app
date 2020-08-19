namespace Demo.API.Respository.Factory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public ILocationRepository LocationRepository { get; set; }
        public IPatientRepository PatientRepository { get; set; }

        public RepositoryFactory(ILocationRepository locationRepository, IPatientRepository patientRepository)
        {
            LocationRepository = locationRepository;
            PatientRepository = patientRepository;
        }
    }
}
