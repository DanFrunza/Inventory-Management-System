using System;
using System.Collections.Generic;
using BusinessLayer.Contracts;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;

namespace BusinessLayer.Services
{
    public class ProdusComandaService : IProdusComandaService
    {
        private readonly IRepository<ProdusComanda> _produsComandaRepository;

        public ProdusComandaService(IRepository<ProdusComanda> produsComandaRepository)
        {
            _produsComandaRepository = produsComandaRepository;
        }

        public void AdaugaProdusComanda(Guid produsId, Guid comandaId, int cantitate)
        {
            var produsComanda = new ProdusComanda(produsId, comandaId, cantitate);
            _produsComandaRepository.Add(produsComanda);
            _produsComandaRepository.Save(); // Asigură-te că modificările sunt salvate
        }

        public IEnumerable<ProdusComanda> ToateProduseleComanda()
        {
            return _produsComandaRepository.GetAll();
        }

        public ProdusComanda GasesteProdusComanda(Guid id)
        {
            return _produsComandaRepository.GetById(id);
        }

        public void StergeProdusComanda(Guid id)
        {
            var produsComanda = _produsComandaRepository.GetById(id);
            if (produsComanda != null)
            {
                _produsComandaRepository.Remove(produsComanda);
                _produsComandaRepository.Save(); // Asigură-te că modificările sunt salvate
            }
        }

        public void ActualizeazaProdusComanda(ProdusComanda produsComanda)
        {
            _produsComandaRepository.Update(produsComanda);
            _produsComandaRepository.Save(); // Asigură-te că modificările sunt salvate
        }

        public void Save()
        {
            _produsComandaRepository.Save();
        }
    }
}
