using App.core;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.Infrastructure
{
    public class DiseaseRepository
    {
        public async Task InsertDiseaseAsync(string diseaseName)
        {
            using (var context = new AppDBContext())
            {
                var newDisease = new Disease
                {
                    DiseaseName = diseaseName
                };
                await context.Diseases.AddAsync(newDisease);
                context.SaveChanges();
            }
        }

        public async Task DeleteDiseaseAsync(int diseaseId)
        {
            using (var context = new AppDBContext())
            {
                var disease = context.Diseases.FirstOrDefault(d => d.DiseaseId == diseaseId);
                if (disease != null)
                {
                    context.Diseases.Remove(disease);
                    context.SaveChanges();
                }
            }
        }

        public async Task UpdateDiseaseAsync(Disease disease)
        {
            using (var context = new AppDBContext())
            {
                var Disease = context.Diseases.FirstOrDefault(d => d.DiseaseId == disease.DiseaseId);
                if (disease != null)
                {
                    Disease.DiseaseName = disease.DiseaseName;
                    context.SaveChanges();
                }
            }
        }

        public async Task<IEnumerable<Disease>> ListDiseases()
        {
            using (var context = new AppDBContext())
            {
                return context.Diseases.ToList();
            }
        }

        public async Task<float> AverageNumberOfDiseases()
        {
            
            using (var context = new AppDBContext())
            {
                var averageDiseasesPerPatient = context.Patients
                .GroupJoin(
                 context.Patient_Disease,          // Join with PatientDisease
                 patient => patient.PatientId,            // Key from the Patient table
                 patientDisease => patientDisease.PatientId, // Key from the PatientDisease table
                 (patient, patientDiseases) => new { patient, patientDiseases }  // Group the results
             )
             .Select(g => g.patientDiseases.Count()).ToList(); // Count the diseases per patient, 0 if none
                var averageChronicDiseases = averageDiseasesPerPatient.Average();
                return (float)averageChronicDiseases;
            }
          
        }
       
        }
    
}

