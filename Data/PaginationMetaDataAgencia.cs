using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiServices.Data
{
    public class PaginationMetaDataAgencia
    {
        public PaginationMetaDataAgencia (int totalCount, int currentPage, int pageSize)
        {
            TotalCount = totalCount;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        public int CurrentPage { get; private set; }
        public int TotalCount  { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }   
}
