using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUCSA.DATA
{
    public class SUCSAContext:DbContext
    {
        public SUCSAContext() : base("SUCSADB")
        {

        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<AcitivityCategory> Categories { get; set; }
    }
}
