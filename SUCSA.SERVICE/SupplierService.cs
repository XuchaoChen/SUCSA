using SUCSA.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUCSA.SERVICE
{
   public  class SupplierService:BaseService
    {
        public SupplierService() : base() { }

        public Supplier GetSupplierByID(int id)
        {
            return context.Suppliers.Find(id);
        }

        public ICollection<Supplier> GetAllSuppliers()
        {
            return context.Suppliers.ToList();
        }

        public ICollection<Supplier> GetAllTopSuppliers()
        {
            return context.Suppliers.Where(x => x.IsTop == true).ToList();
        }

        public bool AddSupplier(Supplier supplier)
        {
            if (supplier != null)
            {
                context.Suppliers.Add(supplier);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteSupplier(int id)
        {
            if (context.Suppliers.Find(id) != null)
            {
                context.Suppliers.Remove(context.Suppliers.Find(id));
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
