using App.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure
{
    public class AppointmentRepository
    {
        public async Task InsertAppointmentAsync(Appointment appointment)
        {
            using (var context = new AppDBContext())
            {
                var newappointment = new Appointment
                {
                    AppointmentDate = appointment.AppointmentDate,
                    Diagnosis=appointment.Diagnosis,
                    Treatment=appointment.Treatment,
                    IsChronic=appointment.IsChronic,
                    Status=appointment.Status,
                    UserId=appointment.UserId,
                    PatientId=appointment.PatientId,
                };
                await context.Appointments.AddAsync(newappointment);
                context.SaveChanges();
            }
        }

        public async Task DeleteAppointmentAsync(int appointmentId)
        {
            using (var context = new AppDBContext()) 
            {
                var appointment=context.Appointments.FirstOrDefault(a=>a.AppointmentId==appointmentId);
                if (appointment != null)
                {
                    context.Appointments.Remove(appointment);
                    context.SaveChanges() ;
                }
            }
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            using(var context = new AppDBContext())
            {
                var Appointment = context.Appointments.FirstOrDefault(a => a.AppointmentId == appointment.AppointmentId);
                if (Appointment != null)
                {
                    Appointment.AppointmentDate = appointment.AppointmentDate;
                    Appointment.Diagnosis = appointment.Diagnosis;
                    Appointment.Treatment = appointment.Treatment;
                    Appointment.Status = appointment.Status;
                    Appointment.UserId = appointment.UserId;
                    Appointment.PatientId = appointment.PatientId;
                    Appointment.IsChronic = appointment.IsChronic;
                    Appointment.Status= appointment.Status;
                    context.SaveChanges();
                }
            }
        }

        public async Task CloseAppointment(int appointmentId, string diagnosis, int isChronic, string treatment)
        {
            using (var context = new AppDBContext())
            {
                var Appointment = context.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);
                Console.WriteLine(Appointment);
                if (Appointment != null)
                {
                    Appointment.Status = 4;
                    Appointment.Diagnosis = diagnosis;
                    Appointment.IsChronic = isChronic;
                    Appointment.Treatment = treatment;
                    context.SaveChanges();
                    if (isChronic == 1)
                    {
                        var patient = context.Patients.FirstOrDefault(p => p.PatientId == Appointment.PatientId);
                        var disease = context.Diseases.FirstOrDefault(d => d.DiseaseName == diagnosis);
                        if (patient != null && disease != null)
                        {
                            PatientDisease patientDisease = new PatientDisease();
                            patientDisease.PatientId = patient.PatientId;
                            patientDisease.DiseaseId = disease.DiseaseId;
                            context.Patient_Disease.Add(patientDisease);
                        }
                        context.SaveChanges();
                    }
                    
                }
            }
        }

        public async Task<IEnumerable<Appointment>> AppointmentsBetweenTwoDates(DateTime startDate, DateTime endDate)
        {
            using(var context = new AppDBContext())
            {
                var appointments = context.Appointments.Where(a => a.AppointmentDate > startDate && a.AppointmentDate < endDate).ToList();
                return appointments;
            }
        }

        public  async Task<IEnumerable<Appointment>> AppointmentsForASpecificDoctor(string firstName, string lastName)
        {
            using(var context = new AppDBContext())
            {
                var appointment=context.Appointments.Where(a=>a.user.FirstName==firstName && a.user.LastName==lastName).ToList();
                return appointment;
            }
        }

        public  async Task<IEnumerable<AppWithPat>> RetrievesAppointmentsWithPatientNamesUsingPagination(int pageNumber, int pageSize)
        {
            using(var context=new AppDBContext())
            {
                var appWithPat=context.Appointments
                    .GroupJoin(
                    context.Patients,
                    appointment=>appointment.PatientId,
                    patient=>patient.PatientId,
                    (appointment,patient)=> new AppWithPat
                    {
                        AppointmentId=appointment.AppointmentId,
                        AppointmentDate=appointment.AppointmentDate,
                        Name=appointment.patient.FirstName+" "+appointment.patient.LastName
                    }
                    ).Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
                return appWithPat;
            }
        }

        public async Task<IEnumerable<Appointment>> ListAppointments()
        {
            using( var context = new AppDBContext())
            {
                return context.Appointments.ToList();
            }
        }
    }
}
