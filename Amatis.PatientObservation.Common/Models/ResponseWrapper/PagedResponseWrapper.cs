using System;

namespace Amatis.PatientObservation.Common.Models.ResponseWrapper
{
    public class PagedResponse
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public object Data { get; set; }
        public PagedResponse(object data, int pageNumber, int pageSize, int totalRecords)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.TotalRecords = totalRecords;
            this.TotalPages = totalRecords / pageSize;
            if (totalRecords % pageSize > 0)
                this.TotalPages++;
        }
    }
}
