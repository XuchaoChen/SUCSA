using SUCSA.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUCSA.Models
{
    public class SupplierViewModels
    {
        public List<Supplier> activities { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
}