using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;

namespace Actris.Infrastructure.Services
{
    public class CaOverdueImpactService : BaseCrudService<CaOverdueImpactDto, CaOverdueImpactDto>, ICaOverdueImpactService
    {
        private readonly ICaOverdueImpactRepository _repo;
        public CaOverdueImpactService(ICaOverdueImpactRepository repo) : base(repo)
        {
            _repo = repo;
        }
    }
}
