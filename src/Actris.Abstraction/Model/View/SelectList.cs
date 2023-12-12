using System.Collections.Generic;
using System.Linq;

namespace Actris.Abstraction.Model.View
{
   public class LookupList
   {

      public string ColumnId { get; set; }
      public List<LookupItem> Items { get; set; }

      public LookupList()
      {
         Items = new List<LookupItem>();
      }

      public LookupList(IEnumerable<LookupItem> items)
      {
         Items = items.GroupBy(o=> new { o.Value, o.Text }).Select(o=>o.First()).OrderBy(o => o.Text).ToList();
      }


      public LookupList(List<string> lsString)
      {
         Items = new List<LookupItem>();
         foreach (var item in lsString)
         {
            Items.Add(new LookupItem
            {
               Text = item,
               Value = item
            });
         }
      }

      public void AddInactiveOptionIfAny(string selectedID, string selectedName)
      {
         if (!Items.Any(o => o.Value == selectedID))
         {
            Items.Add(new LookupItem
            {
               Text = selectedName,
               Value = selectedID
            });
         }
      }

      public string GetText(string selectedValue)
      {
         if (Items == null)
         {
            return null;
         }

         var item = Items.FirstOrDefault(o => o.Value == selectedValue);
         if (item != null)
         {
            return item.Text;
         }

         return null;
      }

      public LookupList Clone()
      {
         return new LookupList
         {
            ColumnId = ColumnId,
            Items = Items.Select(o => new LookupItem(o)).ToList()
         };
      }
   }

   public class LookupItem
   {
      public string Text { get; set; }
      public string Description { get; set; }
      public object Object { get; set; }
      public string Value { get; set; }
      public bool Selected { get; set; }
      public bool IsDisabled { get; set; }

      public LookupItem()
      {

      }

      public LookupItem(string id, string desc)
      {
         Value = id;
         Text = desc;
      }

      public LookupItem(LookupItem from)
      {
         Text = from.Text;
         Value = from.Value;
         IsDisabled = from.IsDisabled;
      }
   }
}
