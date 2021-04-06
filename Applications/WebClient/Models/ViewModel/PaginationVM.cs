using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Models.ViewModel
{
    public class PaginationVM
    {
        public int PageCount { get; private set; }
        public int PageNumber { get; private set; }
        public int SurroundingPagesNumber { get; private set; }

        public PaginationVM(int pageCount, int pageNumber, int surroundingPagesNumbers)
        {
            PageCount = pageCount;
            PageNumber = pageNumber;
            SurroundingPagesNumber = surroundingPagesNumbers;
        }
    }
}