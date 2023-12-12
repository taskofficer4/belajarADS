using Actris.Abstraction.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actris.Abstraction.Repositories
{
    public interface ITxActRepository : ICrudRepository<TxActDto, TxActDto>
    {
        List<TxCaDto> GetCaList(string actRef);
        Task SaveWorkflowResponse(TxActDto dto);
    }
}
