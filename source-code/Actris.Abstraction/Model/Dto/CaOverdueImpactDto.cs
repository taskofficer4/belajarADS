using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using System;
using System.Collections.Generic;

namespace Actris.Abstraction.Model.Dto
{
    public class CaOverdueImpactDto : BaseDtoAutoMapper<MD_CorrectiveActionOverdueImpact>
    {
        public string CorrectiveActionOverdueImpactID { get; set; }
        public string ImpactTitle { get; set; }
        public string ImpactValue { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string DataStatus { get; set; }
        public string ImpactTitle2 { get; set; }
        public string ImpactValue2 { get; set; }

        public CaOverdueImpactDto()
        {
        }

        public CaOverdueImpactDto(MD_CorrectiveActionOverdueImpact entity) : base(entity)
        {
        }

        public static List<ColumnDefinition> GetColumnDefinitions()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("Id", nameof(CorrectiveActionOverdueImpactID), ColumnType.String),
                new ColumnDefinition("Title (en)", nameof(ImpactTitle), ColumnType.String),
                new ColumnDefinition("Value (en)", nameof(ImpactValue), ColumnType.String),
                new ColumnDefinition("Title (id)", nameof(ImpactTitle2), ColumnType.String),
                new ColumnDefinition("Value (id)", nameof(ImpactValue2), ColumnType.String)
            };
        }
    }
}
