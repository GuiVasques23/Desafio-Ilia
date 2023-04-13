using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Sheet.Domain.Models;

namespace Time.Sheet.Domain.Repositories
{
    public class RegistrosRepository : IRegistrosRepository
    {
        public Task InserirAsync(Registro horario)
        {
            throw new NotImplementedException();
        }

        public Task<Registro> ObterRegistroPorDiaAsync(DateTime dia)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Registro>> ObterRegistrosMensaisAsync(string mes)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Registro registro)
        {
            throw new NotImplementedException();
        }
    }
}
