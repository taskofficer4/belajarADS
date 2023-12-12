using AutoMapper;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;

namespace Actris.Web
{
    public class AutoMapperConfig
    {
        public static void RegisterGlobalMapping()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<TX_CorrectiveAction, CorrectiveActionDto>().ReverseMap();
                cfg.CreateMap<MD_ActionTrackingSource, ActSourceDto>().ReverseMap();
                cfg.CreateMap<MD_CorrectiveActionPriority, CaPriorityDto>().ReverseMap();
                cfg.CreateMap<MD_CorrectiveActionImpactAnalysis, ImpactAnalysisDto>().ReverseMap();
                cfg.CreateMap<MD_CorrectiveActionOverdueImpact, CaOverdueImpactDto>().ReverseMap();
            });
        }
    }
}
