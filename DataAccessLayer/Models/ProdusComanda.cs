using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ProdusComanda : BaseEntity
    {
        public Guid ProdusId { get; set; } // Identificatorul produsului asociat comenzii
        public Produs Produs { get; set; } // Produsul asociat comenzii
        public Guid ComandaId { get; set; } // Identificatorul comenzii
        public Comanda Comanda { get; set; } // Comanda asociată
        public int Cantitate { get; set; } // Cantitatea de produs comandată

        public ProdusComanda(Guid produsId, Guid comandaId, int cantitate)
        {
            Id = Guid.NewGuid();
            ProdusId = produsId;
            ComandaId = comandaId;
            Cantitate = cantitate;
        }
    }
}