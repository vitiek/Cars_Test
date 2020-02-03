using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.ViewModels
{
    public class DatatableResultViewModel<T>
    {
        public List<T> Result { get; set; }
        public int RecordsFiltered { get; set; }

    }
}
