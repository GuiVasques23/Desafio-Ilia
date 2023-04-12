using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Sheet.Domain.Models;

namespace Time.Sheet.Domain.Services
{
    public interface IBatidasService
    {
        Task<Registro> InserirBatidaAsync(Momento momento);
        Task<Registro> ObterRegistroPorIdAsync(int id);
    }
}
