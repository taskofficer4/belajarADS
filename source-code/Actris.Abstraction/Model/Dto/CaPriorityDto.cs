using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using System;
using System.Collections.Generic;

namespace Actris.Abstraction.Model.Dto
{
    public class CaPriorityDto : BaseDtoAutoMapper<MD_CorrectiveActionPriority>
    {
        public string CorrectiveActionPriorityID { get; set; }
        public string PriorityTitle { get; set; }
        public string PriorityValue { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string DataStatus { get; set; }
        public string PriorityTitle2 { get; set; }
        public string PriorityValue2 { get; set; }
        public Nullable<int> PriorityDuration { get; set; }

        public CaPriorityDto()
        {
        }

        public CaPriorityDto(MD_CorrectiveActionPriority entity) : base(entity)
        {
        }

        public static List<ColumnDefinition> GetColumnDefinitions()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("Id", nameof(CaPriorityDto.CorrectiveActionPriorityID), ColumnType.String),
                new ColumnDefinition("Title (en)", nameof(CaPriorityDto.PriorityTitle), ColumnType.String),
                new ColumnDefinition("Value (en)", nameof(CaPriorityDto.PriorityValue), ColumnType.String),
                new ColumnDefinition("Title (id)", nameof(CaPriorityDto.PriorityTitle2), ColumnType.String),
                new ColumnDefinition("Value (id)", nameof(CaPriorityDto.PriorityValue2), ColumnType.String),
                new ColumnDefinition("Duration", nameof(CaPriorityDto.PriorityDuration), ColumnType.Number)
            };
        }
    }
}
