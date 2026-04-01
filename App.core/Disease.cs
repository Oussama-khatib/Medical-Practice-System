using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.core
{
    public class Disease
    {
        [Key]
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public ICollection<PatientDisease>? Patients { get; set; }
    }
}
