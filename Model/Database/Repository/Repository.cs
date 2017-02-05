using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Database.Repository
{
    public abstract class Repository
    {
        protected readonly PWDatabase db;
        public Repository(PWDatabase db)
        {
            this.db = db;
        }

        
    }
}
