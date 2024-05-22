using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO
{
    public class ProdusComandaDTO
    {
        public Guid ProdusId { get; set; }
        public Guid ComandaId { get; set; }
        public int Cantitate { get; set; }
    }
}
