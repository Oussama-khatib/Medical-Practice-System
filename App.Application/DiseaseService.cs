using App.core;
using App.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application
{
    public class DiseaseService :IDiseaseService
    {
        private static DiseaseRepository diseaseRepository = new DiseaseRepository();

        public async Task<Disease> CreateDisease(Disease disease)
        {
            await diseaseRepository.InsertDiseaseAsync(disease.DiseaseName);
            return disease;
        }
        public async Task RemoveDisease(int id)
        {
            await diseaseRepository.DeleteDiseaseAsync(id);
        }
        public async Task<Disease?> UpdateDisease(Disease disease)
        {
            bool exist = false;
            var Diseases = await ListDiseases();
            foreach (var Disease in Diseases)
            {
                if (Disease.DiseaseId == disease.DiseaseId)
                {
                    exist = true;
                }
            }
            if (exist == false)
            {
                return null;
            }
            await diseaseRepository.UpdateDiseaseAsync(disease);
            return disease;
        }
        public async Task<float> AverageNumberOfDiseases()
        {
            var average = await diseaseRepository.AverageNumberOfDiseases();
            return average;
        }
        public async Task<IEnumerable<Disease>> ListDiseases()
        {
            var diseases = await diseaseRepository.ListDiseases();
            return diseases;
        }
    }
}
