using BusinessLayer.Contracts;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Services
{
    public class FurnizorService : IFurnizorService
    {
        private readonly IRepository<Furnizor> _repository;

        public FurnizorService(IRepository<Furnizor> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Furnizor> GetAll()
        {
            return _repository.GetAll();
        }

        public Furnizor GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void Add(Furnizor furnizor)
        {
            _repository.Add(furnizor);
            _repository.Save();
        }

        public void Update(Furnizor furnizor)
        {
            _repository.Update(furnizor);
            _repository.Save();
        }

        public void Remove(Furnizor furnizor)
        {
            _repository.Remove(furnizor);
            _repository.Save();
        }

        public bool Exists(Guid id)
        {
            return _repository.GetAll().Any(f => f.Id == id);
        }
    }
}
