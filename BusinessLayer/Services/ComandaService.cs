using BusinessLayer.Contracts;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class ComandaService : IComandaService
    {
        private readonly IRepository<Comanda> _comandaRepository;
        private readonly IRepository<ProdusComanda> _produsComandaRepository;

        public ComandaService(IRepository<Comanda> comandaRepository, IRepository<ProdusComanda> produsComandaRepository)
        {
            _comandaRepository = comandaRepository;
            _produsComandaRepository = produsComandaRepository;
        }

        public void Add(Comanda comanda)
        {
            _comandaRepository.Add(comanda);
        }

        public void AddProdus(Guid comandaId, Guid produsId, int cantitate)
        {
            var produsComanda = new ProdusComanda(produsId, comandaId, cantitate);
            _produsComandaRepository.Add(produsComanda);
        }

        public IEnumerable<Comanda> GetAll()
        {
            return _comandaRepository.GetAll();
        }

        public Comanda GetById(Guid id)
        {
            return _comandaRepository.GetById(id);
        }

        public void Remove(Comanda comanda)
        {
            _comandaRepository.Remove(comanda);
        }

        public void Update(Comanda comanda)
        {
            _comandaRepository.Update(comanda);
        }

        public void Save()
        {
            _comandaRepository.Save();
        }
    }
}
