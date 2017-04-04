using SUCSA.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUCSA.SERVICE
{
    public class BaseService:IDisposable
    {
        protected SUCSAContext context;
        private bool selfCreated;
        public BaseService()
        {
            selfCreated = true;
            context = new SUCSAContext();
        }

        public BaseService(SUCSAContext context)
        {
            if (context != null)
            {
                selfCreated = false;
                this.context = context;
            }
            else
            {
                selfCreated = true;
                this.context = new SUCSAContext();
            }
        }

        public void Dispose()
        {
            if (selfCreated)
            {
                context.Dispose();
            }
        }
    }
}
