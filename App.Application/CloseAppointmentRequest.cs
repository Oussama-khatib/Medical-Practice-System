using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application
{
    public class CloseAppointmentRequest
    {
        public string? Diagnosis {  get; set; }
        public int IsChronic { get; set; }
        public string? Treatment {  get; set; } 

    }
}
