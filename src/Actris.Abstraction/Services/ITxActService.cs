using Actris.Abstraction.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actris.Abstraction.Services
{
   public interface ITxActService : ICrudService<TxActDto, TxActDto>
   {
      List<TxCaDto> GetCaList(string actRef);
      Task<TxActDto> GetOneWithoutCa(string id);
    }
}
