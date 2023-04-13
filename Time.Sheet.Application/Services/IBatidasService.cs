using Time.Sheet.Domain.Models;
using Time.Sheet.Domain.Utils;

namespace Time.Sheet.Domain.Services
{
    public interface IBatidasService
    {
        Task<ResponseResult> InserirBatidaAsync(Momento momento);
    }
}
