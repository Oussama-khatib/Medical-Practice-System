using App.core;
using App.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application
{
    public class AppointmentService : IAppointmentService
    {
        private static AppointmentRepository appointmentRepository = new AppointmentRepository();
        private static UserService userService = new UserService();
        private static PatientService patientService = new PatientService();
        public async Task<Appointment?> CreateAppointment(Appointment appointment)
        {
            if (appointment.Status > 4 || appointment.Status < 0)
            { 
                return null;
            }
            bool exist = false;
            var Users = await userService.ListUsers();
            foreach (var user in Users)
            {
                if (user.UserId == appointment.UserId)
                {
                    exist = true;
                }
            }
            if (exist == false) { appointment.UserId = -1; return appointment; }
            bool existe = false;
            var Patients = await patientService.ListPatients();
            foreach (var patient in Patients)
            {
                if (patient.PatientId == appointment.PatientId)
                {
                    existe = true;
                }
            }
            if (existe == false) { appointment.PatientId = -1; return appointment; }
            await appointmentRepository.InsertAppointmentAsync(appointment);
            return appointment;
        }

        public  async Task RemoveAppointment(int id)
        {
            await appointmentRepository.DeleteAppointmentAsync(id);
            Console.WriteLine("Appointment Deleted");
        }

        public async Task<Appointment?> UpdateAppointment(Appointment appointment)
        {
            if (appointment.Status > 4 || appointment.Status < 0)
            {
                return null;
            }
            bool exist = false;
            var Users = await userService.ListUsers();
            foreach (var user in Users)
            {
                if (user.UserId == appointment.UserId)
                {
                    exist = true;
                }
            }
            if (exist == false) { appointment.UserId = -1; return appointment; }
            bool existe = false;
            var Patients = await patientService.ListPatients();
            foreach (var patient in Patients)
            {
                if (patient.PatientId == appointment.PatientId)
                {
                    existe = true;
                }
            }
            if (existe == false) { appointment.PatientId = -1; return appointment; }
            await appointmentRepository.UpdateAppointmentAsync(appointment);
            return appointment;
        }

        public async Task<int?> CloseAppointment(int appointmentId, string diagnosis, int isChronic, string treatment)
        {
            if (isChronic != 1 && isChronic != 0)
            {
                Console.WriteLine("IsChronic is 1 or 0"); return null;
            }
            await appointmentRepository.CloseAppointment(appointmentId, diagnosis, isChronic, treatment);
            return 1;
        }

        public async Task<IEnumerable<Appointment>?> AppointmentsBetweenTwoDates(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return null;
            }
            var appointments = await appointmentRepository.AppointmentsBetweenTwoDates(startDate, endDate);
            return appointments;
        }

        public async Task<IEnumerable<Appointment>> AppointmentsForASpecificDoctor(string firstName, string lastName)
        {
            var appointments = await appointmentRepository.AppointmentsForASpecificDoctor(firstName, lastName);
            return appointments;
        }

        public async Task<IEnumerable<AppWithPat>?> RetrievesAppointmentsWithPatientNamesUsingPagination(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                return null;
            }
            if (pageSize < 1)
            {
                return null;
            }
            var multi = await appointmentRepository.RetrievesAppointmentsWithPatientNamesUsingPagination(pageNumber, pageSize);
            return multi;
        }

        public async Task<IEnumerable<Appointment>> ListAppointments()
        {
            var appointments = await appointmentRepository.ListAppointments();
            return appointments;
        }
    }
}
