using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;

namespace Actris.Infrastructure.Services
{
    public class MdCaPriorityService : BaseCrudService<MdCaPriorityDto, MdCaPriorityDto>, IMdCaPriorityService
    {
        private readonly IMdCaPriorityRepository _repo;
        public MdCaPriorityService(IMdCaPriorityRepository repo) : base(repo)
        {
            _repo = repo;
        }

        public bool IsPriorityExist(string priority)
        {
            return _repo.IsPriorityExist(priority);
        }
    }
}
