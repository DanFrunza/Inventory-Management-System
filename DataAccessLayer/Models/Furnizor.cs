using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Furnizor : BaseEntity
    {
        public string Nume { get; set; } // Numele furnizorului
        public string Contact { get; set; } // Informațiile de contact ale furnizorului
        public string Adresa { get; set; } // Adresa furnizorului
        public ICollection<Produs> Produse { get; set; } // Lista de produse furnizate de către furnizor

        public Furnizor(string nume, string contact, string adresa)
        {
            Id = Guid.NewGuid();
            Nume = nume;
            Contact = contact;
            Adresa = adresa;
            Produse = new List<Produs>(); 
        }
    }
}
