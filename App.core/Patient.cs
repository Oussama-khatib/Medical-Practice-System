using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.core
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string ?LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
        public string? Adress { get; set; }
        public ICollection<PatientDisease>? Diseases { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
