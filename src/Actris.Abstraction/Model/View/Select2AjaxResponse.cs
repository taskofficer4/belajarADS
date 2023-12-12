using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Vml.Office;
using Actris.Abstraction.Model.Entities;
using System.Linq.Expressions;
using System;
using Actris.Abstraction.Model.DbView;

namespace Actris.Abstraction.Model.View
{
    public class Select2AjaxResponse
    {
        public int TotalItem { get; set; }
        public bool IsMoreItem { get; set; }
        public List<VwEmployeeLs> Items { get; set; }
    }

}
