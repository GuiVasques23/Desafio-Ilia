using Time.Sheet.Domain.Models;

namespace Time.Sheet.Domain.Services
{
    public interface IFolhasDePontoService
    {
        Task<Relatorio> GerarRelatorioMensalAsync(string mes);
    }
}
