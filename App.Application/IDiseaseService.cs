using App.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application
{
    public interface IDiseaseService
    {
        Task<Disease> CreateDisease(Disease disease);
        Task RemoveDisease(int id);
        Task<Disease?> UpdateDisease(Disease disease);
        Task<float> AverageNumberOfDiseases();
        Task<IEnumerable<Disease>> ListDiseases();
    }
}
