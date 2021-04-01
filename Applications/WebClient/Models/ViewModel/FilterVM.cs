using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Models.ViewModel
{
    public class FilterVM
    {
        public string SearchFilter { get; private set; }

        public FilterVM(string searchFilter)
        {
            SearchFilter = searchFilter;
        }
    }
}