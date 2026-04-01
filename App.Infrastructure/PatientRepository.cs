using App.core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure
{
    public class PatientRepository
    {
        public async Task InsertPatientAsync(Patient patient)
        {
            using (var context = new AppDBContext())
            {
                var newPatient = new Patient
                {
                     FirstName= patient.FirstName,
                     LastName= patient.LastName,
                     DateOfBirth=patient.DateOfBirth,
                     Email=patient.Email,
                     ContactNumber=patient.ContactNumber,
                     Adress=patient.Adress,
                };
                await context.Patients.AddAsync(newPatient);
                context.SaveChanges();
            }
        }

        public async Task DeletePatientAsync(int patientId)
        {
            using (var context = new AppDBContext()) 
            {
                var patient = context.Patients.FirstOrDefault(p=>p.PatientId == patientId);
                if (patient != null) 
                {
                    context.Patients.Remove(patient);
                    context.SaveChanges();
                }
            }
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            using (var context = new AppDBContext())
            {
                var Patient = context.Patients.FirstOrDefault(p => p.PatientId == patient.PatientId);
                if(Patient != null)
                {
                    Patient.FirstName= patient.FirstName;
                    Patient.LastName= patient.LastName;
                    Patient.Adress= patient.Adress;
                    Patient.Email= patient.Email;
                    Patient.DateOfBirth= patient.DateOfBirth;
                    Patient.ContactNumber= patient.ContactNumber;
                    context.SaveChanges();
                }
            }
        }

        public async Task<IEnumerable<PatientCountResult>> PatientNameWithDiseaseCount()
        {
            using (var context = new AppDBContext()) 
            {
                var patientCountResult = context.Patients
                .GroupJoin(
                 context.Patient_Disease,          // Join with PatientDisease
                 patient => patient.PatientId,            // Key from the Patient table
                 patientDisease => patientDisease.PatientId, // Key from the PatientDisease table
                 (patient, patientDiseases) => new PatientCountResult
                 {
                     Name = patient.FirstName + " " + patient.LastName,
                     Diseases = patientDiseases.Count()
                 }  // Group the results
             )
             .ToList();
                return patientCountResult;
            }
        }

        public async Task<IEnumerable<PatientCountResult>> PatientNameWithDiseaseCountGreaterThan5()
        {
            using (var context = new AppDBContext())
            {
               var patientCountResult = context.Patients
                .GroupJoin(
                 context.Patient_Disease,          // Join with PatientDisease
                 patient => patient.PatientId,            // Key from the Patient table
                 patientDisease => patientDisease.PatientId, // Key from the PatientDisease table
                 (patient, patientDiseases) => new PatientCountResult
                 {
                     Name = patient.FirstName + " " + patient.LastName,
                     Diseases = patientDiseases.Count()
                 }  // Group the results
             ).Where(g=>g.Diseases>5)
             .ToList();
                return patientCountResult; 
            }
        }

        public async Task<IEnumerable<Patient>> PatientsWithNoHistory()
        {
            using (var context = new AppDBContext()) 
            {
                var patients = context.Patients.Include(p=>p.Appointments).Where(p=>p.Appointments==null||p.Appointments.Count== 0).ToList();
                return patients;   
            }
        }

        public async Task<IEnumerable<Patient>> ListPatients()
        {
            using(var context = new AppDBContext())
            {
                return context.Patients.ToList();
            }
        }
    }
}
