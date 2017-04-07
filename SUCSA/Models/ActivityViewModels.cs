using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SUCSA.DATA;

namespace SUCSA.Models
{
    public class ActivityViewModels
    {
        public List<Activity> activities { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
}