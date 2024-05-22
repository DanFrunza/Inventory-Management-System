using BusinessLayer.Contracts;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Services
{
    public class DepartamentService : IDepartamentService
    {
        private readonly IRepository<Departament> _repository;

        public DepartamentService(IRepository<Departament> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Departament> GetAll()
        {
            return _repository.GetAll();
        }

        public Departament GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void Add(Departament departament)
        {
            _repository.Add(departament);
            _repository.Save();
        }

        public void Update(Departament departament)
        {
            _repository.Update(departament);
            _repository.Save();
        }

        public void Remove(Departament departament)
        {
            _repository.Remove(departament);
            _repository.Save();
        }

        public bool Exists(Guid id)
        {
            return _repository.GetAll().Any(d => d.Id == id);
        }
    }
}
