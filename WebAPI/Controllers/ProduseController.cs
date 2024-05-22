using BusinessLayer.Contracts;
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
    public class ProduseController : ControllerBase
    {
        private readonly IProdusService _produsService;
        private readonly IFurnizorService _furnizorService;
        private readonly IDepartamentService _departamentService;

        public ProduseController(
             IProdusService produsService,
             IFurnizorService furnizorService,
             IDepartamentService departamentService)
        {
            _produsService = produsService;
            _furnizorService = furnizorService;
            _departamentService = departamentService;
        }

        // GET: api/Produse
        [HttpGet]
        public ActionResult<IEnumerable<ProdusDTO>> GetProduse()
        {
            var produse = _produsService.GetAll().Select(produs => new ProdusDTO
            {
                Nume = produs.Nume,
                Descriere = produs.Descriere,
                Cantitate = produs.Cantitate,
                DataExpirare = produs.DataExpirare,
                FurnizorId = produs.FurnizorId,
                DepartamentId = produs.DepartamentId
            });

            return Ok(produse);
        }

        // GET: api/Produse/{id}
        [HttpGet("{id}")]
        public ActionResult<ProdusDTO> GetProdus(Guid id)
        {
            var produs = _produsService.GetById(id);
            if (produs == null)
            {
                return NotFound();
            }

            var produsDTO = new ProdusDTO
            {
                Nume = produs.Nume,
                Descriere = produs.Descriere,
                Cantitate = produs.Cantitate,
                DataExpirare = produs.DataExpirare,
                FurnizorId = produs.FurnizorId,
                DepartamentId = produs.DepartamentId
            };

            return Ok(produsDTO);
        }

        [HttpPost]
        public ActionResult<ProdusDTO> PostProdus(ProdusDTO produsDTO)
        {
            var furnizor = _furnizorService.GetById(produsDTO.FurnizorId);
            var departament = _departamentService.GetById(produsDTO.DepartamentId);

            if (furnizor == null || departament == null)
            {
                return BadRequest("Furnizorul sau departamentul specificat nu există.");
            }

            var produs = new Produs
            (
                produsDTO.Nume,
                produsDTO.Descriere,
                produsDTO.Cantitate,
                produsDTO.DataExpirare,
                produsDTO.FurnizorId,
                produsDTO.DepartamentId
            );

            _produsService.Add(produs);

            return CreatedAtAction(nameof(GetProdus), new { id = produs.Id }, produsDTO);
        }

        // PUT: api/Produse/{id}
        [HttpPut("{id}")]
        public IActionResult PutProdus(Guid id, ProdusDTO produsDTO)
        {
            var produsToUpdate = _produsService.GetById(id);

            if (produsToUpdate == null)
            {
                return NotFound();
            }

            // Verificăm dacă există modificări pentru câmpurile non-nule
            if (!string.IsNullOrEmpty(produsDTO.Nume))
            {
                produsToUpdate.Nume = produsDTO.Nume;
            }

            if (!string.IsNullOrEmpty(produsDTO.Descriere))
            {
                produsToUpdate.Descriere = produsDTO.Descriere;
            }

            if (produsDTO.Cantitate != 0)
            {
                produsToUpdate.Cantitate = produsDTO.Cantitate;
            }

            if (produsDTO.FurnizorId!=null)
            {
                var furnizor = _furnizorService.GetById(produsDTO.FurnizorId);
                if (furnizor == null)
                {
                    return BadRequest("Furnizorul specificat nu există.");
                }
                produsToUpdate.FurnizorId = produsDTO.FurnizorId;
            }

            if (produsDTO.DepartamentId!=null)
            {
                var departament = _departamentService.GetById(produsDTO.DepartamentId);
                if (departament == null)
                {
                    return BadRequest("Departamentul specificat nu există.");
                }   
                produsToUpdate.DepartamentId = produsDTO.DepartamentId;
            }

            try
            {
                _produsService.Update(produsToUpdate);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_produsService.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // DELETE: api/Produse/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProdus(Guid id)
        {
            var produs = _produsService.GetById(id);
            if (produs == null)
            {
                return NotFound();
            }

            _produsService.Remove(produs);

            return NoContent();
        }
    }
}
