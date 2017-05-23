using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUCSA.DATA
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx=new SUCSAContext())
            {
                var admins = new List<Admin>
                {
                    new Admin() {UserName="admin1",PassWord="admin1" },
                    new Admin() {UserName="admin2",PassWord="admin2" }
                };

                var categories = new List<AcitivityCategory>
                {
                    new AcitivityCategory() {CategoryName="BasketBall" },
                    new AcitivityCategory() {CategoryName="Tennis" }
                };
                var activities = new List<Activity>
                {

                    
                    new Activity(){ CategoryId=1, IsTop=true,PictureName="1",Picture=ByteHelper.ImageToByteArray(ByteHelper.LoadImageFromDisk(@"F:\C#\SUCSA\SUCSA\img\Activities\1.jpg")) },
                    new Activity(){CategoryId=1,IsTop=true,PictureName="2",Picture=ByteHelper.ImageToByteArray(ByteHelper.LoadImageFromDisk(@"F:\C#\SUCSA\SUCSA\img\Activities\2.jpg")) },
                    new Activity(){CategoryId=1,IsTop=true,PictureName="3",Picture=ByteHelper.ImageToByteArray(ByteHelper.LoadImageFromDisk(@"F:\C#\SUCSA\SUCSA\img\Activities\3.jpg")) },
                    new Activity(){CategoryId=1,IsTop=true,PictureName="4",Picture=ByteHelper.ImageToByteArray(ByteHelper.LoadImageFromDisk(@"F:\C#\SUCSA\SUCSA\img\Activities\4.jpg")) },
                    new Activity(){CategoryId=1,IsTop=true,PictureName="5",Picture=ByteHelper.ImageToByteArray(ByteHelper.LoadImageFromDisk(@"F:\C#\SUCSA\SUCSA\img\Activities\5.jpg")) },
                };
                foreach(AcitivityCategory a in categories)
                {
                    ctx.Categories.Add(a);
                }
                foreach(Activity a in activities)
                {
                    ctx.Activities.Add(a); 
                }
                foreach(Admin a in admins)
                {
                    ctx.Admins.Add(a);
                }
                ctx.SaveChanges();
            }
        }
    }
}
