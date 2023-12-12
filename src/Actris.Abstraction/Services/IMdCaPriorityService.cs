using Actris.Abstraction.Model.Dto;

namespace Actris.Abstraction.Services
{
    public interface IMdCaPriorityService : ICrudService<MdCaPriorityDto, MdCaPriorityDto>
    {
        bool IsPriorityExist(string priority);
    }
}
