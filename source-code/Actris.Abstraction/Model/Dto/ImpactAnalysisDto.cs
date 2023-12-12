using Actris.Abstraction.Model.Entities;
using Actris.Abstraction.Model.View;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Web.Util;

namespace Actris.Abstraction.Model.Dto
{
    public class ImpactAnalysisDto : BaseDtoAutoMapper<MD_CorrectiveActionImpactAnalysis>
    {
        public string CorrectiveActionImpactAnalysisID { get; set; }
        public string AnalysisTitle { get; set; }
        public string AnalysisValue { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string DataStatus { get; set; }
        public string AnalysinTitle2 { get; set; }
        public string AnalysisValue2 { get; set; }

        public ImpactAnalysisDto()
        {
        }

        public ImpactAnalysisDto(MD_CorrectiveActionImpactAnalysis entity) : base(entity)
        {
        }
        public static List<ColumnDefinition> GetColumnDefinitions()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("Id", nameof(ImpactAnalysisDto.CorrectiveActionImpactAnalysisID), ColumnType.String),
                new ColumnDefinition("Title (en)", nameof(ImpactAnalysisDto.AnalysisTitle), ColumnType.String),
                new ColumnDefinition("Value (en)", nameof(ImpactAnalysisDto.AnalysisValue), ColumnType.String),
                new ColumnDefinition("Title (id)", nameof(ImpactAnalysisDto.AnalysinTitle2), ColumnType.String),
                new ColumnDefinition("Value (id)", nameof(ImpactAnalysisDto.AnalysisValue2), ColumnType.String)
            };
        }
    }
}
