using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;


namespace Actris.Infrastructure.Services
{
    public class ImpactAnalysisService : BaseCrudService<ImpactAnalysisDto, ImpactAnalysisDto>, IImpactAnalysisService
    {
        private readonly IImpactAnalysisRepository _repo;
        public ImpactAnalysisService(IImpactAnalysisRepository repo) : base(repo)
        {
            _repo = repo;
        }
    }
}
