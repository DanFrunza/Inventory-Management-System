using DataAccessLayer.Models;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Contracts
{
    public interface IComandaService
    {
        void Add(Comanda comanda);
        void AddProdus(Guid comandaId, Guid produsId, int cantitate);
        IEnumerable<Comanda> GetAll();
        Comanda GetById(Guid id);
        void Remove(Comanda comanda);
        void Update(Comanda comanda);
        void Save(); 
    }
}
