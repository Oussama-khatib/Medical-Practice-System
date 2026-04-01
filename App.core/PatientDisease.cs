using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.core
{
    public class PatientDisease
    {
        public int Id { get; set; }

        [ForeignKey("patient")]
        public int PatientId { get; set; }
        public Patient patient { get; set; }

        [ForeignKey("disease")]
        public int DiseaseId { get; set; }
        public Disease disease { get; set; }
    }
}
