using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Actris.Abstraction.Model.Dto;
using Actris.Abstraction.Model.View;

namespace Actris.Web.Extensions
{
    public static class SelectListItemExtension
    {


        public static SelectList ToSelectList(this LookupList lookupList)
        {
            if (lookupList == null)
            {
                lookupList = new LookupList();
            }

            var result = new SelectList(lookupList.Items.ToList(), "Value", "Text");

            return result;
        }



        public static List<SelectListItem> ToSelectListWithDisabledItems(this LookupList lookupList)
        {
            var lstItem = new List<SelectListItem>();

            foreach (var lookupListItem in lookupList.Items)
            {
                lstItem.Add(new SelectListItem
                {
                    Value = lookupListItem.Value,
                    Disabled = lookupListItem.IsDisabled,
                    Text = lookupListItem.Text
                });
            }
            return lstItem;
        }

        public static SelectList ToSelectList(this List<string> listString)
        {
            var listItems = listString.Select(a => new SelectListItem
            {
                Text = a,
                Value = a,
            });

            var result = new SelectList(listItems, "Text", "Text", null);

            return result;
        }

        public static SelectList ToSelectList(this List<EmployeeDto> empList)
        {
            if (empList == null)
            {
                return new SelectList(new List<SelectListItem>());
            }
            var result = new SelectList(empList, "EmpId", "EmpName");
            return result;
        }

 
    }
}