using SUCSA.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUCSA.Models
{
    public class HomePageViewModels
    {
        public IList<Activity> activities { get; set; }
        public IList<Supplier> suppliers { get; set; }
        public HomePageViewModels(IList<Activity> activities, IList<Supplier> suppliers)
        {
            this.activities = activities;
            this.suppliers = suppliers;
        }


    }
}