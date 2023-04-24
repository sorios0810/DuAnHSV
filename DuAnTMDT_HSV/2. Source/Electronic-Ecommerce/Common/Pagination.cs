using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electronic_Ecommerce.Common
{
    public class Pagination
    {
        public string Display { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool IsActive { get; set; }
        public int PageIndex { get; set; }
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get { return (int)Math.Ceiling((double)TotalItems / Page); } }
        public bool HasPreviousPage { get { return PageIndex > 1; } }
        public bool HasNextPage { get { return PageIndex < TotalPages; } }
        public List<object> Items { get; set; }
    }
}