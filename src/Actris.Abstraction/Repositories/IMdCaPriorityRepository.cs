using Actris.Abstraction.Model.Dto;

namespace Actris.Abstraction.Repositories
{
    public interface IMdCaPriorityRepository : ICrudRepository<MdCaPriorityDto, MdCaPriorityDto>
    {      
        bool IsPriorityExist(string priority);
    }
}
