using Time.Sheet.Domain.Models;
using Time.Sheet.Infraestructure.Repositories;

namespace Time.Sheet.Domain.Services
{
    public class FolhasDePontoService : IFolhasDePontoService
    {
        private readonly IRegistrosRepository _registrosRepository;

        public FolhasDePontoService(IRegistrosRepository registrosRepository)
        {
            _registrosRepository = registrosRepository ?? throw new ArgumentNullException(nameof(registrosRepository));
        }

        public Task<Relatorio> GerarRelatorioMensalAsync(string mes)
        {
            var registroList = _registrosRepository.ObterRegistrosMensaisAsync(mes);
            if (registroList == null)
            {
                return null;
            }

            return null;
        }
    }
}
