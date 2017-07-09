using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUCSA.DATA;
using System.Security.Cryptography;
using System.IdentityModel;

namespace SUCSA.SERVICE
{
    public class AdminService:BaseService
    {
        public AdminService() : base() { }

        public Admin GetAdminByName(string name)
        {
            return context.Admins.Where(x => x.UserName == name).FirstOrDefault();
        }

        public bool AddAdmin(Admin admin)
        {
            if (admin != null)
            {
                admin.PassWord = Encrypt(admin.PassWord);
                context.Admins.Add(admin);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ChangePassword(int id,string password)
        {
            var admin = context.Admins.Find(id);
            if (admin != null)
            {
                admin.PassWord =Encrypt(password);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteAdmin(int id)
        {
            var admin = context.Admins.Find(id);
            if (admin != null)
            {
                context.Admins.Remove(admin);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Admin> GetAdminsInARange(int currentPage, int maxRows)
        {
            return context.Admins.OrderBy(x => x.AdminID).Skip((currentPage - 1) * maxRows).Take(maxRows).ToList();
        }

        public int CountAdmins()
        {
            return context.Admins.Count();
        }

        private string Encrypt(string password)
        {
            var data = Encoding.Unicode.GetBytes(password);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encrypted);
        }
        
        public static string Decrypt(string password)
        {
            byte[] data = Convert.FromBase64String(password);
            byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(decrypted);
        }
    }
}
