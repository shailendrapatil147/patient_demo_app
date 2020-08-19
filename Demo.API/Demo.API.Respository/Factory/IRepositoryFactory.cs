namespace Demo.API.Respository.Factory
{
    public interface IRepositoryFactory
    {
        ILocationRepository LocationRepository { get; set; }
        IPatientRepository PatientRepository { get; set; }
    }
}
