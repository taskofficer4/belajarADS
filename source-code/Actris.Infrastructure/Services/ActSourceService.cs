using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;
using System.Threading.Tasks;

namespace Actris.Infrastructure.Services
{
    public class ActSourceService : BaseCrudService<ActSourceDto, ActSourceDto>, IActSourceService
    {
        private readonly IActSourceRepository _repo;
        public ActSourceService(IActSourceRepository repo) : base(repo)
        {
            _repo = repo;
        }
    }
}
