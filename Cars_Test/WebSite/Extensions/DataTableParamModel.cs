using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Extensions
{
    //Handle data from datatbles.js response
    public class DataTableParamModel
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DTSearch Search { get; set; }
        public IEnumerable<DTOrder> Order { get; set; }
        public IEnumerable<DTColumns> Columns { get; set; }
    }

    public class DTSearch
    {
        public string Value { get; set; }
    }

    public class DTOrder
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }

    public class DTColumns
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public DTSearch Search { get; set; }
    }
}
