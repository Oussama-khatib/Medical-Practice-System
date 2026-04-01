using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.core
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public int? IsChronic { get; set; }
        public int Status { get; set; }

        [ForeignKey("user")]
        public int UserId { get; set; }
        public User? user { get; set; }

        [ForeignKey("patient")]
        public int PatientId { get; set; }
        public Patient? patient { get; set; }

    }
}
