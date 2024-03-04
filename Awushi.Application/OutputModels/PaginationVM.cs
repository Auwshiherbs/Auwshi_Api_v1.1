using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.OutputModels
{
    public class PaginationVM<T>
    {
        public int CurrenPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalNumberOfRecords { get; set; }
        public List<T> Items { get; set; }
        public bool HasPrevious => CurrenPage>=0;
        public bool HasNext => CurrenPage<=TotalPages;

        public PaginationVM(int currenPage, int totalPages, int pageSize, int totalNumberOfRecords, List<T> items)
        {
            CurrenPage = currenPage;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalNumberOfRecords = totalNumberOfRecords;
            Items = items;

        }


    }
}
