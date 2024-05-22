using System;
using System.Collections.Generic;

namespace BusinessLayer.DTO
{
    public class ComandaDTO
    {
        public string Nume { get; set; } // Numele comenzii
        public DateTime Data { get; set; }
        public ICollection<ProdusComandaDTO> ProdusComenzi { get; set; }

        
    }

}