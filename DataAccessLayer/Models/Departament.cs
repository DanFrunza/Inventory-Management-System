using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Departament : BaseEntity
    {
        public string Nume { get; set; } // Numele departamentului
        public ICollection<Produs> Produse { get; set; } // Lista de produse asociate cu acest departament
        public Departament(string nume)
        {
            Id = Guid.NewGuid(); 
            Nume = nume;
            Produse = new List<Produs>();
        }
    }
}
