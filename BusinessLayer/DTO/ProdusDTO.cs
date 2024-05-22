using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ProdusDTO
{
    public string Nume { get; set; }
    public string Descriere { get; set; }
    public int Cantitate { get; set; }
    public DateTime DataExpirare { get; set; }
    public Guid FurnizorId { get; set; }
    public Guid DepartamentId { get; set; }
}
