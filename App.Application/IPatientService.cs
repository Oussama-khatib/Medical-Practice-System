using App.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application
{
    public interface IPatientService
    {
        Task<Patient?> CreatePatient(Patient patient);
        Task RemovePatient(int id);
        Task<Patient?> UpdatePatient(Patient patient);
        Task<IEnumerable<PatientCountResult>> PatientNameWithDiseaseCount();
        Task<IEnumerable<PatientCountResult>> PatientNameWithDiseaseCountGreaterThan5();
        Task<IEnumerable<Patient>> PatientsWithNoHistory();
        Task<IEnumerable<Patient>> ListPatients();
    }
}
