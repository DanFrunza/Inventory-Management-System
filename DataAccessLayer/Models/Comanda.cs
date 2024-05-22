using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Comanda : BaseEntity
    {
        public string Nume { get; set; } // Numele comenzii
        public DateTime Data { get; set; } // Data comenzii
        public ICollection<ProdusComanda> ProdusComenzi { get; set; } // Lista de produse comandate și cantitatea corespunzătoare

        public Comanda(string nume, DateTime data)
        {
            Id = Guid.NewGuid();
            Nume = nume;
            Data = data;
            ProdusComenzi = new List<ProdusComanda>();
        }
    }
}
