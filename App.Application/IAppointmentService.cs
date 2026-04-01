using App.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application
{
    public interface IAppointmentService
    {
        Task<Appointment?> CreateAppointment(Appointment appointment);
        Task RemoveAppointment(int id);
        Task<Appointment?> UpdateAppointment(Appointment appointment);
        Task<int?> CloseAppointment(int appointmentId, string diagnosis, int isChronic, string treatment);
        Task<IEnumerable<Appointment>?> AppointmentsBetweenTwoDates(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Appointment>> AppointmentsForASpecificDoctor(string firstName, string lastName);
        Task<IEnumerable<AppWithPat>?> RetrievesAppointmentsWithPatientNamesUsingPagination(int pageNumber, int pageSize);
        Task<IEnumerable<Appointment>> ListAppointments();
    }
}
