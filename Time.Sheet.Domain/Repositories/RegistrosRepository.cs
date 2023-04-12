using System;
using System.Collections.Generic;
using System.Linq;
using Time.Sheet.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Time.Sheet.Domain.Repositories
{
    public class RegistrosRepository : IRegistrosRepository
    {
        private readonly PontoDbContext _dbContext;

        public RegistrosRepository(PontoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InserirAsync(Registro registro)
        {
            await _dbContext.Registros.AddAsync(registro);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Registro registro)
        {
            _dbContext.Entry(registro).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            var registro = await ObterPorIdAsync(id);
            if (registro != null)
            {
                _dbContext.Registros.Remove(registro);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Registro>> ObterRegistrosMensaisAsync(DateTime mes)
        {
            return await _dbContext.Registros
                .Where(r => r.Dia.Year == mes.Year && r.Dia.Month == mes.Month)
                .ToListAsync();
        }

        public async Task<Registro> ObterRegistroPorDiaAsync(DateTime dia)
        {
            return await _dbContext.Registros
                .Include(r => r.Horarios)
                .FirstOrDefaultAsync(r => r.Dia == dia);
        }

        public async Task<Registro> ObterPorIdAsync(int id)
        {
            return await _dbContext.Registros.FindAsync(id);
        }
    }
}
