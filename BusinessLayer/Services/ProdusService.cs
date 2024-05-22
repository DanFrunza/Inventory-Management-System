using BusinessLayer.Contracts;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Services
{
    public class ProdusService : IProdusService
    {
        private readonly IRepository<Produs> _repository;

        public ProdusService(IRepository<Produs> repository)
        {
            _repository = repository;
        }

        public int CountRecords()
        {
            return _repository.GetAll().Count();
        }

        public IEnumerable<Produs> GetAll()
        {
            return _repository.GetAll();
        }

        public Produs GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void Add(Produs produs)
        {
            _repository.Add(produs);
            _repository.Save();
        }

        public void Update(Produs produs)
        {
            _repository.Update(produs);
            _repository.Save();
        }

        public void Remove(Produs produs)
        {
            _repository.Remove(produs);
            _repository.Save();
        }

        public bool Exists(Guid id)
        {
            return _repository.GetAll().Any(p => p.Id == id);
        }
    }
}
