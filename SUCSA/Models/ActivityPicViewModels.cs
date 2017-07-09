using SUCSA.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUCSA.Models
{
    public class ActivityPicViewModels
    {
        public IList<List<Activity>> activities { get; set; }
        public ActivityPicViewModels(IList<Activity> activities)
        {
            this.activities = activities.GroupBy(s => s.CategoryId)
                .OrderBy(g => g.Key)
                .Select(g => g.ToList())
                .ToList();
        }
    }
}