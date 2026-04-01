using App.Application;
using App.core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
        {
            var createdAppointment = await _appointmentService.CreateAppointment(appointment);
            if (createdAppointment == null)
            {
                return BadRequest("Invalid appointment details or status.");
            }
            if (createdAppointment.UserId == -1)
            {
                return BadRequest("Invalid user ID.");
            }
            if (createdAppointment.PatientId == -1)
            {
                return BadRequest("Invalid patient ID.");
            }
            return Ok(createdAppointment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAppointment(int id)
        {
            await _appointmentService.RemoveAppointment(id);
            return Ok("Appointment deleted successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id,[FromBody] Appointment appointment)
        {
            appointment.AppointmentId = id;
            var updatedAppointment = await _appointmentService.UpdateAppointment(appointment);
            if (updatedAppointment == null)
            {
                return BadRequest("Invalid appointment details or status.");
            }
            if (updatedAppointment.UserId == -1)
            {
                return BadRequest("Invalid user ID.");
            }
            if (updatedAppointment.PatientId == -1)
            {
                return BadRequest("Invalid patient ID.");
            }
            return Ok(updatedAppointment);
        }

        [HttpPost("close/{id}")]
        public async Task<IActionResult> CloseAppointment(int id, [FromBody] CloseAppointmentRequest request)
        {
            var result = await _appointmentService.CloseAppointment(id, request.Diagnosis, request.IsChronic, request.Treatment);
            if (result == null)
            {
                return BadRequest("Invalid IsChronic value. It must be 0 or 1.");
            }
            return Ok("Appointment closed successfully.");
        }

        [HttpGet("between")]
        public async Task<IActionResult> GetAppointmentsBetweenTwoDates([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var appointments = await _appointmentService.AppointmentsBetweenTwoDates(startDate, endDate);
            if (appointments == null)
            {
                return BadRequest("Start date must be earlier than end date.");
            }
            return Ok(appointments);
        }

        [HttpGet("doctor")]
        public async Task<IActionResult> GetAppointmentsForASpecificDoctor([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var appointments = await _appointmentService.AppointmentsForASpecificDoctor(firstName, lastName);
            return Ok(appointments);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetAppointmentsWithPatientNamesUsingPagination([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var appointments = await _appointmentService.RetrievesAppointmentsWithPatientNamesUsingPagination(pageNumber, pageSize);
            if (appointments == null)
            {
                return BadRequest("Invalid pagination parameters.");
            }
            return Ok(appointments);
        }

        [HttpGet]
        public async Task<IActionResult> ListAppointments()
        {
            var appointments = await _appointmentService.ListAppointments();
            return Ok(appointments);
        }
    }
}
