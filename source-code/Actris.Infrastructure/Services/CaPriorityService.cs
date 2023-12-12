using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;


namespace Actris.Infrastructure.Services
{
    public class CaPriorityService : BaseCrudService<CaPriorityDto, CaPriorityDto>, ICaPriorityService
    {
        private readonly ICaPriorityRepository _repo;
        public CaPriorityService(ICaPriorityRepository repo) : base(repo)
        {
            _repo = repo;
        }
    }
}
