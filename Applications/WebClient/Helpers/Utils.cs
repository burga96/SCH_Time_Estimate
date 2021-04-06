using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applications.WebClient.Helpers
{
    public static class Utils
    {
        public static int CalculatePageCount(int totalCount, int pageSize)
        {
            int pageCount = (totalCount % pageSize == 0) ?
                (totalCount / pageSize) :
                (totalCount / pageSize) + 1;
            return pageCount;
        }
    }
}