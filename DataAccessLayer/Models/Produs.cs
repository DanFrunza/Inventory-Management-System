using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Produs : BaseEntity
    {
        public string Nume { get; set; } // Numele produsului
        public string Descriere { get; set; } // Descrierea produsului
        public int Cantitate { get; set; } // Cantitatea disponibilă a produsului
        public DateTime DataExpirare { get; set; } // Data de expirare a produsului
        public Guid FurnizorId { get; set; } // Identificatorul furnizorului care livrează produsul
        public Furnizor Furnizor { get; set; } // Furnizorul care livrează produsul
        public Guid DepartamentId { get; set; } // Identificatorul departamentului din care face parte produsul
        public Departament Departament { get; set; } // Departamentul din care face parte produsul
        public ICollection<ProdusComanda> ProdusComenzi { get; set; } // Lista de comenzi care includ acest produs și cantitatea corespunzătoare

        public Produs(string nume, string descriere, int cantitate, DateTime dataExpirare, Guid furnizorId, Guid departamentId)
        {
            Id = Guid.NewGuid();
            Nume = nume;
            Descriere = descriere;
            Cantitate = cantitate;
            DataExpirare = dataExpirare;
            FurnizorId = furnizorId;
            DepartamentId = departamentId;
            ProdusComenzi = new List<ProdusComanda>();
        }
    }
}
