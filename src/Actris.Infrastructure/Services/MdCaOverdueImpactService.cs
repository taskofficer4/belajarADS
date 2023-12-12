using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;

namespace Actris.Infrastructure.Services
{
   public class MdCaOverdueImpactService : BaseCrudService<MdCaOverdueImpactDto, MdCaOverdueImpactDto>, IMdCaOverdueImpactService
   {
      private readonly IMdCaOverdueImpactRepository _repo;
      public MdCaOverdueImpactService(IMdCaOverdueImpactRepository repo) : base(repo)
      {
         _repo = repo;
      }

      public bool IsExist(string id)
      {
         return _repo.IsExist(id);
      }


   }
}
