using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.ValueObjects
{
    public class ResultsAndTotalCount<ResultType>
    {
        public ICollection<ResultType> Results { get; private set; }

        public int TotalCount { get; private set; }

        public ResultsAndTotalCount(
            ICollection<ResultType> resultCollection,
            int totalCount
        )
        {
            Results = resultCollection;
            TotalCount = totalCount;
        }
    }
}