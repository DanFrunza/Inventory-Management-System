using BusinessLayer.Contracts;
using BusinessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamenteController : ControllerBase
    {
        private readonly IDepartamentService _departamentService;

        public DepartamenteController(IDepartamentService departamentService)
        {
            _departamentService = departamentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DepartamentDTO>> GetDepartamente()
        {
            var departamente = _departamentService.GetAll().Select(departament => new DepartamentDTO
            {
                Nume = departament.Nume
            });

            return Ok(departamente);
        }

        [HttpGet("{id}")]
        public ActionResult<DepartamentDTO> GetDepartament(Guid id)
        {
            var departament = _departamentService.GetById(id);
            if (departament == null)
            {
                return NotFound();
            }

            var departamentDTO = new DepartamentDTO
            {
                Nume = departament.Nume
            };

            return Ok(departamentDTO);
        }

        [HttpPost]
        public ActionResult<DepartamentDTO> PostDepartament(DepartamentDTO departamentDTO)
        {
            var departament = new Departament(departamentDTO.Nume);

            _departamentService.Add(departament);

            return CreatedAtAction(nameof(GetDepartament), new { id = departament.Id }, departamentDTO);
        }

        [HttpPut("{id}")]
        public IActionResult PutDepartament(Guid id, DepartamentDTO departamentDTO)
        {
            var departamentToUpdate = _departamentService.GetById(id);

            if (departamentToUpdate == null)
            {
                return NotFound();
            }

            departamentToUpdate.Nume = departamentDTO.Nume;

            _departamentService.Update(departamentToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartament(Guid id)
        {
            var departament = _departamentService.GetById(id);
            if (departament == null)
            {
                return NotFound();
            }

            _departamentService.Remove(departament);

            return NoContent();
        }
    }
}
