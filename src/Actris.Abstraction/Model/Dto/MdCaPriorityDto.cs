using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using System;
using System.Collections.Generic;

namespace Actris.Abstraction.Model.Dto
{
    public class MdCaPriorityDto : BaseDtoAutoMapper<MD_CorrectiveActionPriority>
    {
        public string Id { get; set; }
        public string Priority { get; set; }
        public string PriorityBahasa { get; set; }
        public Nullable<int> PriorityDuration { get; set; }
        public string DataStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }


        public MdCaPriorityDto()
        {
        }

        public MdCaPriorityDto(MD_CorrectiveActionPriority entity) : base(entity)
        {
        }

        public static List<ColumnDefinition> GetColumnDefinitions()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("Priority", nameof(MdCaPriorityDto.Priority), ColumnType.String),
                new ColumnDefinition("Priority (Bahasa)", nameof(MdCaPriorityDto.PriorityBahasa), ColumnType.String),
                new ColumnDefinition("Duration", nameof(MdCaPriorityDto.PriorityDuration), ColumnType.Number)
            };
        }
    }
}
