using System.Collections.Generic;

namespace Actris.Abstraction.Model.View
{
    public class CrudPage
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public List<string> Path { get; set; }
        public GridParam GridParam { get; set; }
    }
}
