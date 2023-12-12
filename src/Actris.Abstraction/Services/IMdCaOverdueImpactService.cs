using Actris.Abstraction.Model.Dto;

namespace Actris.Abstraction.Services
{
    public interface IMdCaOverdueImpactService : ICrudService<MdCaOverdueImpactDto, MdCaOverdueImpactDto>
    {
        bool IsExist(string id);
    }
}
