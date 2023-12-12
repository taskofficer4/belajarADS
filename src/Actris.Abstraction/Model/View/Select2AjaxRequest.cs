using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Vml.Office;
using Actris.Abstraction.Model.Entities;
using System.Linq.Expressions;
using System;

namespace Actris.Abstraction.Model.View
{
    public class Select2AjaxRequest
    {
        public string term { get; set; }
        public string q { get; set; }
        public int page { get; set; }
    }

}
