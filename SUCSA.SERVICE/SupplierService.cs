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

        public List<Supplier> GetSupplierInARange(int currentPage, int maxRows)
        {
            return context.Suppliers.OrderBy(x=>x.SupplierID).Skip((currentPage - 1) * maxRows).Take(maxRows).ToList(); 
        }

        public int CountSuppliers()
        {
            return context.Suppliers.Count();
        }

        public bool updateSupplier(Supplier supplier)
        {
            var result = context.Suppliers.Find(supplier.SupplierID);
            if (result != null)
            {
                result.SupplierName = supplier.SupplierName;
                result.Description = supplier.Description;
                result.IsTop = supplier.IsTop;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool RemoveSupplier(Supplier supplier)
        {
            if (context.Suppliers.Remove(supplier) == null)
            {
                return false;
            }
            context.SaveChanges();
            return true;
        }
    }
}
