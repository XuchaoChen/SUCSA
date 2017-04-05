using SUCSA.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUCSA.SERVICE
{
    public class ActivitiesService:BaseService
    {
        public ActivitiesService() : base() { }

        public ICollection<Activity> GetAllActivities()
        {
            return context.Activities.ToList();
        }

        public Activity GetActivityByName(string name)
        {
            return context.Activities.Where(x => x.PictureName == name).FirstOrDefault();
        }

        public Activity GetActivityById(int id)
        {
            return context.Activities.Find(id);
        }

        public bool updateActivity(Activity activity)
        {
            var result = context.Activities.Find(activity.PictureID);
            if(result != null)
            {
                result.PictureName = activity.PictureName;
                result.Description = activity.Description;
                result.IsTop = activity.IsTop;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public ICollection<Activity> GetAllTopActivities()
        {
            return context.Activities.Where(x => x.IsTop == true).ToList();
        }

        public bool AddActivities(ICollection<Activity> activities)
        {
            foreach(Activity activity in activities)
            {
                if (context.Activities.Add(activity) == null)
                {
                    return false;
                }
            }
            context.SaveChanges();
            return true;
        }


        public bool RemoveActivity(Activity activity)
        {
            if (context.Activities.Remove(activity) == null)
            {
                return false;
            }
            context.SaveChanges();
            return true;
        }

        public bool RemoveActiviites(ICollection<Activity> activities)
        {
            foreach(Activity activity in activities)
            {
                if (context.Activities.Remove(activity) == null)
                {
                    return false;
                }
            }
            context.SaveChanges();
            return true;
        }

        public List<string> getAllCategory()
        {
            return context.Activities.Select(x => x.Category).Distinct().ToList();
        }
    }
}
