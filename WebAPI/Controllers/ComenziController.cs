using BusinessLayer.Contracts;
using BusinessLayer.DTO;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Services;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComenziController : ControllerBase
    {
        private readonly IComandaService _comandaService;
        private readonly IProdusComandaService _produsComandaService;
        private readonly IProdusService _produsService;


        public ComenziController(IComandaService comandaService, IProdusComandaService produsComandaService, IProdusService produsService)
        {
            _comandaService = comandaService;
            _produsComandaService = produsComandaService;
            _produsService = produsService; 
        }

        [HttpGet]
        public ActionResult<IEnumerable<ComandaDTO>> GetComenzi()
        {
            var comenzi = _comandaService.GetAll().Select(comanda => new ComandaDTO
            {
                Nume = comanda.Nume,
                Data = comanda.Data,
                ProdusComenzi = _produsComandaService.ToateProduseleComanda() // Interogare în baza de date pentru a aduce ProdusComenzi
                    .Where(pc => pc.ComandaId == comanda.Id) // Filtrare pentru produsele asociate comenzii curente
                    .Select(pc => new ProdusComandaDTO
                    {
                        ProdusId = pc.ProdusId,
                        ComandaId = pc.ComandaId,
                        Cantitate = pc.Cantitate
                    }).ToList()
            });

            return Ok(comenzi);
        }





        [HttpGet("{id}")]
        public ActionResult<ComandaDTO> GetComanda(Guid id)
        {
            var comanda = _comandaService.GetById(id);
            if (comanda == null)
            {
                return NotFound();
            }

            var produsComenzi = _produsComandaService.ToateProduseleComanda()
                .Where(pc => pc.ComandaId == id)
                .Select(pc => new ProdusComandaDTO
                {
                    ProdusId = pc.ProdusId,
                    ComandaId = pc.ComandaId,
                    Cantitate = pc.Cantitate
                }).ToList();

            var comandaDTO = new ComandaDTO
            {
                Nume = comanda.Nume,
                Data = comanda.Data,
                ProdusComenzi = produsComenzi
            };

            return Ok(comandaDTO);
        }


        [HttpPost]
        public ActionResult<ComandaDTO> PostComanda(CreateComandaDTO createComandaDTO)
        {
            // Generați data actuală
            var dataComanda = DateTime.Now;

            // Verificați dacă lista de produse este validă
            if (createComandaDTO.ProduseIds == null || !createComandaDTO.ProduseIds.Any())
            {
                return BadRequest("Lista de produse nu poate fi goală.");
            }

            try
            {
                // Creați comanda
                var comanda = new Comanda(createComandaDTO.Nume, dataComanda);
                _comandaService.Add(comanda);

                var produsComenzi = new List<ProdusComanda>();

                // Adăugați produsele la comandă
                foreach (var produsId in createComandaDTO.ProduseIds)
                {
                    var produs = _produsService.GetById(produsId);
                    if (produs == null)
                    {
                        return BadRequest($"Produsul cu id-ul {produsId} nu există.");
                    }

                    // Adăugăm produsul în lista de ProdusComanda asociată comenzii
                    var produsComanda = new ProdusComanda(produsId, comanda.Id, 1); // presupunând o cantitate implicită de 1
                    _produsComandaService.AdaugaProdusComanda(produsId, comanda.Id, 1);
                    produsComenzi.Add(produsComanda);
                }

                comanda.ProdusComenzi = produsComenzi;
                _comandaService.Update(comanda);

                // Creează un DTO pentru comanda creată
                var comandaDTO = new ComandaDTO
                {
                    Nume = comanda.Nume,
                    Data = comanda.Data,
                    ProdusComenzi = comanda.ProdusComenzi.Select(pc => new ProdusComandaDTO
                    {
                        ProdusId = pc.ProdusId,
                        ComandaId = pc.ComandaId,
                        Cantitate = pc.Cantitate
                    }).ToList()
                };

                // Returnați răspunsul HTTP cu DTO-ul comandă creată
                return CreatedAtAction(nameof(GetComanda), new { id = comanda.Id }, comandaDTO);
            }
            catch (Exception ex)
            {
                // În cazul în care apare o excepție, returnați un cod de eroare internă a serverului
                return StatusCode(500, $"A apărut o eroare internă: {ex.Message}");
            }
        }



        [HttpPut("{id}")]
        public IActionResult PutComanda(Guid id, ComandaDTO comandaDTO)
        {
            var comandaToUpdate = _comandaService.GetById(id);

            if (comandaToUpdate == null)
            {
                return NotFound();
            }

            _comandaService.Update(comandaToUpdate);

            if (comandaDTO.ProdusComenzi != null)
            {
                foreach (var produsComandaDTO in comandaDTO.ProdusComenzi)
                {
                    var existingProdusComanda = comandaToUpdate.ProdusComenzi.FirstOrDefault(pc => pc.ProdusId == produsComandaDTO.ProdusId);
                    if (existingProdusComanda != null)
                    {
                        // Verificare și actualizare pentru cantitate
                        if (produsComandaDTO.Cantitate != 0)
                        {
                            existingProdusComanda.Cantitate = produsComandaDTO.Cantitate;
                        }
                    }
                    else
                    {
                        // Verificare și adăugare pentru id produs
                        if (produsComandaDTO.ProdusId != Guid.Empty)
                        {
                            _produsComandaService.AdaugaProdusComanda(produsComandaDTO.ProdusId, comandaToUpdate.Id, produsComandaDTO.Cantitate);
                        }
                    }
                }
            }

            return NoContent();
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteComanda(Guid id)
        {
            var comanda = _comandaService.GetById(id);
            if (comanda == null)
            {
                return NotFound();
            }

            _comandaService.Remove(comanda);

            return NoContent();
        }
    }
}
