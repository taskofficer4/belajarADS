using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Response;
using Actris.Abstraction.Model.View;
using Actris.Abstraction.Repositories;
using Actris.Abstraction.Services;
using System.Threading.Tasks;

namespace Actris.Infrastructure.Services
{
    public class MdActSourceService : BaseCrudService<MdActSourceDto, MdActSourceDto>, IMdActSourceService
    {
        private readonly IMdActSourceRepository _repo;
        public MdActSourceService(IMdActSourceRepository repo) : base(repo)
        {
            _repo = repo;
        }

        public override async Task<MdActSourceDto> GetOne(string id)
        {
            MdActSourceDto result =  await _repo.GetOne(id);
            result.CompositeKey = result.ToKeyString();
            return result;
        }

        public override async Task<Paged<MdActSourceDto>> GetPaged(GridParam param)
        {
            var result = await _repo.GetPaged(param);

            // kumpulin key nya dalam 1 string karena composite key
            foreach (var item in result.Items)
            {
                item.CompositeKey = item.ToKeyString();
            }
            return result;
        }

    }
}
