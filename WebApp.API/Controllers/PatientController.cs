using App.Application;
using App.core;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Service;

namespace WebApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
        {
            var createdPatient = await _patientService.CreatePatient(patient);
            if (createdPatient == null)
            {
                return BadRequest("Invalid patient details.");
            }
            return Ok(createdPatient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovePatient(int id)
        {
            await _patientService.RemovePatient(id);
            return Ok("Patient deleted successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] Patient patient)
        {
            patient.PatientId = id;
            var updatedPatient = await _patientService.UpdatePatient(patient);
            if (updatedPatient == null)
            {
                return BadRequest("Invalid patient details.");
            }
            return Ok(updatedPatient);
        }

        [HttpGet("diseaseCount")]
        public async Task<IActionResult> GetPatientNameWithDiseaseCount()
        {
            var result = await _patientService.PatientNameWithDiseaseCount();
            return Ok(result);
        }

        [HttpGet("diseaseCountGreaterThan5")]
        public async Task<IActionResult> GetPatientNameWithDiseaseCountGreaterThan5()
        {
            var result = await _patientService.PatientNameWithDiseaseCountGreaterThan5();
            return Ok(result);
        }

        [HttpGet("noHistory")]
        public async Task<IActionResult> GetPatientsWithNoHistory()
        {
            var patients = await _patientService.PatientsWithNoHistory();
            return Ok(patients);
        }

       
        [HttpGet]
        public async Task<IActionResult> ListPatients()
        {
            var patients = await _patientService.ListPatients();
            return Ok(patients);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] App.core.LoginRequest login)
        {
            var patients = await _patientService.ListPatients();
            var patient = patients.FirstOrDefault(u =>
                u.Email == login.Email);

            if (patient == null)
                return Unauthorized("Invalid credentials");

            // ✅ Generate JWT token for this user
            var token = TokenService.GenerateTokenPatient(patient);

            return Ok(new
            {
                message = "Login successful",
                token,
                patient = new { patient.PatientId,patient.FirstName,patient.LastName,patient.DateOfBirth, patient.Email,patient.ContactNumber,patient.Adress }
            });
        }
    }
}
