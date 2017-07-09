using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SUCSA.DATA
{
    class Program
    {
        static void Main(string[] args)
        {
            string base_path = Environment.CurrentDirectory;
            int index = base_path.LastIndexOf("\\");
            base_path = base_path.Substring(0, index);
            index = base_path.LastIndexOf("\\");
            base_path = base_path.Substring(0, index);
            index = base_path.LastIndexOf("\\");
            base_path = base_path.Substring(0, index+1);
            using (var ctx=new SUCSAContext())
            {
                var admins = new List<Admin>
                {
                    new Admin() {UserName="admin1",PassWord=Encrypt("admin1") },
                    new Admin() {UserName="admin2",PassWord=Encrypt("admin2") }
                };

                var categories = new List<AcitivityCategory>
                {
                    new AcitivityCategory() {CategoryName="BasketBall" },
                    new AcitivityCategory() {CategoryName="Tennis" }
                };
                var activities = new List<Activity>
                {

                    
                    new Activity(){CategoryId=1,IsTop=true,PictureName="1",Picture=ByteHelper.ImageToByteArray(ByteHelper.LoadImageFromDisk(base_path+@"SUCSA\img\Activities\1.jpg")) },
                    new Activity(){CategoryId=1,IsTop=true,PictureName="2",Picture=ByteHelper.ImageToByteArray(ByteHelper.LoadImageFromDisk(base_path+@"SUCSA\img\Activities\2.jpg")) },
                    new Activity(){CategoryId=1,IsTop=true,PictureName="3",Picture=ByteHelper.ImageToByteArray(ByteHelper.LoadImageFromDisk(base_path+@"SUCSA\img\Activities\3.jpg")) },
                    new Activity(){CategoryId=1,IsTop=true,PictureName="4",Picture=ByteHelper.ImageToByteArray(ByteHelper.LoadImageFromDisk(base_path+@"SUCSA\img\Activities\4.jpg")) },
                    new Activity(){CategoryId=1,IsTop=true,PictureName="5",Picture=ByteHelper.ImageToByteArray(ByteHelper.LoadImageFromDisk(base_path+@"SUCSA\img\Activities\5.jpg")) },
                };

                var suppliers = new List<Supplier>
                {
                    new Supplier(){ SupplierName = "chef", Description="chef", Picture=ByteHelper.ImageToByteArray(ByteHelper.LoadImageFromDisk(base_path+@"SUCSA\img\supplier\chef_logo.png")), IsTop=false},
                    new Supplier(){ SupplierName = "kiva", Description="kiva", Picture=ByteHelper.ImageToByteArray(ByteHelper.LoadImageFromDisk(base_path+@"SUCSA\img\supplier\kiva_logo.png")), IsTop=false}
                };
                foreach(AcitivityCategory a in categories)
                {
                    ctx.Categories.Add(a);
                }
                foreach(Activity a in activities)
                {
                    ctx.Activities.Add(a); 
                }
                foreach(Supplier s in suppliers)
                {
                    ctx.Suppliers.Add(s);
                }
                foreach(Admin a in admins)
                {
                    ctx.Admins.Add(a);
                }
                ctx.SaveChanges();
            }
        }

        static string Encrypt(string password)
        {
            var data = Encoding.Unicode.GetBytes(password);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encrypted);
        }
    }
}
