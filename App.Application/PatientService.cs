using App.core;
using App.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application
{
    public class PatientService : IPatientService
    {
        private static PatientRepository patientRepository = new PatientRepository();

        public async Task<Patient?> CreatePatient(Patient patient)
        {
            if (patient.FirstName ==null || patient.LastName == null || patient.ContactNumber == null || patient.Email == null || patient.DateOfBirth == null || patient.Adress == null)
            {
                return null;
            }
            string firstName = patient.FirstName;
            patient.FirstName = char.ToUpper(firstName[0]) + firstName.Substring(1);
            string lastName = patient.LastName;
            patient.LastName = char.ToUpper(lastName[0]) + lastName.Substring(1);
            await patientRepository.InsertPatientAsync(patient);
            return patient;
        }

        public async Task RemovePatient(int id)
        {
            await patientRepository.DeletePatientAsync(id);
        }

        public async Task<Patient?> UpdatePatient(Patient patient)
        {
            if (patient.FirstName == null || patient.LastName == null || patient.ContactNumber == null || patient.Email == null || patient.DateOfBirth == null || patient.Adress == null)
            {
                return null;
            }
            string firstName = patient.FirstName;
            patient.FirstName = char.ToUpper(firstName[0]) + firstName.Substring(1);
            string lastName = patient.LastName;
            patient.LastName = char.ToUpper(lastName[0]) + lastName.Substring(1);
            await patientRepository.UpdatePatientAsync(patient);
            return patient;
        }

        public async Task<IEnumerable<PatientCountResult>> PatientNameWithDiseaseCount()
        {
            var patientCountResult = await patientRepository.PatientNameWithDiseaseCount();
            return patientCountResult;
        }

        public async Task<IEnumerable<PatientCountResult>> PatientNameWithDiseaseCountGreaterThan5()
        {
            var patientCountResult = await patientRepository.PatientNameWithDiseaseCountGreaterThan5();
            return patientCountResult;
        }

        public async Task<IEnumerable<Patient>> PatientsWithNoHistory()
        {
            var patients = await patientRepository.PatientsWithNoHistory();
            return patients;
        }

        public async Task<IEnumerable<Patient>> ListPatients()
        {
            var patients = await patientRepository.ListPatients();
            return patients;
        }
    }
}
