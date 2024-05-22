using System;
using System.Collections.Generic;

namespace BusinessLayer.DTO
{
    public class CreateComandaDTO
    {
        public string Nume { get; set; } // Numele comenzii
        public List<Guid> ProduseIds { get; set; } // Lista de ID-uri ale produselor
    }
}
