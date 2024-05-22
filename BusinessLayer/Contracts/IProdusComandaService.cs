using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models;

namespace BusinessLayer.Contracts
{
    public interface IProdusComandaService
    {
        void AdaugaProdusComanda(Guid produsId, Guid comandaId, int cantitate);
        IEnumerable<ProdusComanda> ToateProduseleComanda();
        ProdusComanda GasesteProdusComanda(Guid id);
        void StergeProdusComanda(Guid id);
        void ActualizeazaProdusComanda(ProdusComanda produsComanda);
        void Save(); 
    }
}
