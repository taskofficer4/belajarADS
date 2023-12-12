using AutoMapper;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.Entities;

namespace Actris.Web
{
   public class AutoMapperConfig
   {
      public static void RegisterGlobalMapping()
      {
         Mapper.Initialize(cfg =>
         {
            cfg.CreateMap<TX_Attachment, FileAttachmentDto>().ReverseMap();
            cfg.CreateMap<TX_CorrectiveActionUser, TxCaUserDto>().ReverseMap();
            cfg.CreateMap<TxCaDto, TX_CorrectiveAction>()
             .ForMember(o => o.MD_CorrectiveActionPriority, opt => opt.Ignore())
             .ReverseMap();

            cfg.CreateMap<TxActDto, TX_ActionTracking>()
               .ForMember(o => o.MD_ActionTrackingSource, opt => opt.Ignore())
               .ReverseMap();

            cfg.CreateMap<MD_ActionTrackingSource, MdActSourceDto>().ReverseMap();
            cfg.CreateMap<MD_CorrectiveActionOverdueImpact, MdCaOverdueImpactDto>().ReverseMap();
            cfg.CreateMap<MD_CorrectiveActionPriority, MdCaPriorityDto>().ReverseMap();
         });
      }
   }
}
