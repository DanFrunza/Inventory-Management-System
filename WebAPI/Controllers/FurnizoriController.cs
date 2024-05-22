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
    public class FurnizoriController : ControllerBase
    {
        private readonly IFurnizorService _furnizorService;

        public FurnizoriController(IFurnizorService furnizorService)
        {
            _furnizorService = furnizorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FurnizorDTO>> GetFurnizori()
        {
            var furnizori = _furnizorService.GetAll().Select(furnizor => new FurnizorDTO
            {
                Nume = furnizor.Nume,
                Contact = furnizor.Contact,
                Adresa = furnizor.Adresa
            });

            return Ok(furnizori);
        }

        [HttpGet("{id}")]
        public ActionResult<FurnizorDTO> GetFurnizor(Guid id)
        {
            var furnizor = _furnizorService.GetById(id);
            if (furnizor == null)
            {
                return NotFound();
            }

            var furnizorDTO = new FurnizorDTO
            {
                Nume = furnizor.Nume,
                Contact = furnizor.Contact,
                Adresa = furnizor.Adresa
            };

            return Ok(furnizorDTO);
        }

        [HttpPost]
        public ActionResult<FurnizorDTO> PostFurnizor(FurnizorDTO furnizorDTO)
        {
            var furnizor = new Furnizor(furnizorDTO.Nume, furnizorDTO.Contact, furnizorDTO.Adresa);

            _furnizorService.Add(furnizor);

            return CreatedAtAction(nameof(GetFurnizor), new { id = furnizor.Id }, furnizorDTO);
        }

        [HttpPut("{id}")]
        public IActionResult PutFurnizor(Guid id, FurnizorDTO furnizorDTO)
        {
            var furnizorToUpdate = _furnizorService.GetById(id);

            if (furnizorToUpdate == null)
            {
                return NotFound();
            }
            if(furnizorDTO.Nume!="")
            {
                furnizorToUpdate.Nume = furnizorDTO.Nume;
            }
            if(furnizorDTO.Contact!="")
            {
                furnizorToUpdate.Contact = furnizorDTO.Contact;
            }
            if(furnizorDTO.Adresa!="")
            {
                furnizorToUpdate.Adresa = furnizorDTO.Adresa;
            }
            

            _furnizorService.Update(furnizorToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFurnizor(Guid id)
        {
            var furnizor = _furnizorService.GetById(id);
            if (furnizor == null)
            {
                return NotFound();
            }

            _furnizorService.Remove(furnizor);

            return NoContent();
        }
    }
}
