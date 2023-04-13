using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time.Sheet.Domain.Models;
using Time.Sheet.Infraestructure;

namespace Time.Sheet.Infraestructure.Repositories
{
    public class RegistrosRepository : IRegistrosRepository
    {
        private readonly PontoDbContext _context;

        public RegistrosRepository(PontoDbContext context)
        {
            _context = context;
        }

        public async Task InserirAsync(Registro registro)
        {
            await _context.Registros.AddAsync(registro);
            await _context.SaveChangesAsync();
        }

        public async Task<Registro> ObterRegistroPorDiaAsync(DateTime dia)
        {
            return await _context.Registros.FirstOrDefaultAsync(r => r.Dia == dia);
        }

        public async Task<IEnumerable<Registro>> ObterRegistrosMensaisAsync(string mes)
        {
            var dataInicial = DateTime.Parse(mes + "-01");
            var dataFinal = dataInicial.AddMonths(1).AddDays(-1);
            return await _context.Registros.Where(r => r.Dia >= dataInicial && r.Dia <= dataFinal).ToListAsync();
        }

        public async Task UpdateAsync(Registro registro)
        {
            _context.Entry(registro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
