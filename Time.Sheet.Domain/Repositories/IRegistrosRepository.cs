using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Sheet.Domain.Models;

namespace Time.Sheet.Domain.Repositories
{
    public interface IRegistrosRepository
    {
        Task InserirAsync(Registro registro);
        Task AtualizarAsync(Registro registro);
        Task ExcluirAsync(int id);
        Task<IEnumerable<Registro>> ObterRegistrosMensaisAsync(DateTime mes);
        Task<Registro> ObterRegistroPorDiaAsync(DateTime dia);
        Task<Registro> ObterPorIdAsync(int id);
    }
}
