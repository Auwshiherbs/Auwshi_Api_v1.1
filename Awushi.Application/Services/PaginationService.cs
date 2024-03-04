using AutoMapper;
using Awushi.Application.InputModels;
using Awushi.Application.OutputModels;
using Awushi.Application.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.Services
{
    public class PaginationService<T, S> : IPaginationService<T, S> where T : class
    {
        private readonly IMapper _mapper;
        public PaginationService(IMapper mapper)
        {
            _mapper = mapper;
            
        }
        public PaginationVM<T> GetPagination(List<S> source, PaginationInputModel pagination)
        {
            var currenPage = pagination.PageNumber;
            var pageSize = pagination.PageSize;
            var TotalNumberOfRecords = source.Count;
            var totalPages = (int)Math.Ceiling(TotalNumberOfRecords / (double)pageSize);
            var result = source
                .Skip((pagination.PageNumber - 1) * (pagination.PageSize))
                .Take(pagination.PageSize)
                .ToList();
            var items = _mapper.Map<List<T>>(result);
            PaginationVM<T> paginationVM = new PaginationVM<T>(currenPage, totalPages, pageSize, TotalNumberOfRecords, items);
            return paginationVM;
        }
    }
}
