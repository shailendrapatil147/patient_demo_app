
using AutoMapper;
using Demo.Api.Contracts.Models;
using iMedOneDB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Api.Mapper.EntityToModelMapper
{
    public class EntityToModelMapper
    {
        private static IMapper mapper = ConfigMapper();

        public static IMapper ConfigMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tblcity, City>().ReverseMap();
                cfg.CreateMap<Tblstate, State>().ReverseMap();
                cfg.CreateMap<TBLPATIENT, Patient>().ReverseMap();
            });
            return mapper = config.CreateMapper();
        }

        public static City MapTblCityIntoCity(Tblcity city)
        {
            return mapper.Map<Tblcity, City>(city);
        }

        public static List<City> MapTblCityIntoCity(List<Tblcity> cities)
        {
            return mapper.Map< List<Tblcity>, List<City>>(cities);
        }

        public static State MapTblStateIntoState(Tblstate state)
        {
            return mapper.Map<Tblstate, State>(state);
        }

        public static List<State> MapTblStateIntoState(List<Tblstate> states)
        {
            return mapper.Map<List<Tblstate>, List<State>>(states);
        }

        public static Patient MapTblPatientIntoPatient(TBLPATIENT patient)
        {
            return mapper.Map<TBLPATIENT, Patient>(patient);
        }

        public static TBLPATIENT MapTblPatientIntoPatient(Patient patient)
        {
            return mapper.Map<Patient, TBLPATIENT>(patient);
        }

        public static List<Patient> MapTblPatientIntoPatient(List<TBLPATIENT> patients)
        {
            return mapper.Map<List<TBLPATIENT>, List<Patient>>(patients);
        }
    }
}
