using Microsoft.AspNetCore.Mvc;
using CW11.Services;
using CW11.Models;

namespace CW11.Controllers
{
    [Route("api/hospital")]
    [ApiController]
    public class PrzychodniaController : ControllerBase
    {
        private readonly IPrzychodniaDbService _context;
        public PrzychodniaController(IPrzychodniaDbService context)
        {
            _context = context;
        }

        [HttpGet("doctors")]
        public IActionResult GetDoctor()
        {
            return Ok(_context.GetDoctors());
        }


        [HttpGet("add")]
        public IActionResult AddDoctor(Doctor doctor)
        {
            return Ok(_context.AddDoctor(doctor));
        }


        [HttpGet("modify")]
        public IActionResult ModifyDoctor(Doctor doctor)
        {
            string response = _context.ModifyDoctor(doctor);
            if (response.Equals("Data changed"))
                return Ok(response);
            else
                return BadRequest(response);
        }


        [HttpGet("remove")]
        public IActionResult DropDoctor(int IdDoctor)
        {
            string response = _context.DropDoctor(IdDoctor);
            if (response.Equals("Doctor removed"))
                return Ok(response);
            else
                return BadRequest(response);
        }


    }
}
