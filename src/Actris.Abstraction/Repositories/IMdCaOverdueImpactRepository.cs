using Actris.Abstraction.Model.Dto;

namespace Actris.Abstraction.Repositories
{
    public interface IMdCaOverdueImpactRepository : ICrudRepository<MdCaOverdueImpactDto, MdCaOverdueImpactDto>
    {
      bool IsExist(string overdueImpact);
   }
}
