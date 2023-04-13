using Time.Sheet.Domain.Models;

namespace Time.Sheet.Infraestructure.Repositories
{
    public interface IRegistrosRepository
    {
        Task InserirAsync(Registro registro);
        Task<Registro> ObterRegistroPorDiaAsync(DateTime dia);
        Task<IEnumerable<Registro>> ObterRegistrosMensaisAsync(string mes);

        Task UpdateAsync(Registro registro);
    }
}
