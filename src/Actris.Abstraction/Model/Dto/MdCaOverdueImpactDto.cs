using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using System;
using System.Collections.Generic;

namespace Actris.Abstraction.Model.Dto
{
    public class MdCaOverdueImpactDto : BaseDtoAutoMapper<MD_CorrectiveActionOverdueImpact>
    {
      public string Id { get; set; }
      public string OverdueImpact { get; set; }
      public string ImpactBahasa { get; set; }
      public string DataStatus { get; set; }
      public string CreatedBy { get; set; }
      public Nullable<System.DateTime> CreatedDate { get; set; }
      public MdCaOverdueImpactDto()
        {
        }

        public MdCaOverdueImpactDto(MD_CorrectiveActionOverdueImpact entity) : base(entity)
        {
        }

        public static List<ColumnDefinition> GetColumnDefinitions()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("Overdue Impact", nameof(MdCaOverdueImpactDto.OverdueImpact), ColumnType.String),
                new ColumnDefinition("Impact (Bahasa)", nameof(MdCaOverdueImpactDto.ImpactBahasa), ColumnType.String)
            };
        }
    }
}
