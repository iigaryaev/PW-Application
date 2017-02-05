using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Database.Repository
{
    public class PaymentStateRepository : Repository
    {
        public PaymentStateRepository(PWDatabase db) : base(db)
        {
        }
        
        public IEnumerable<PaymentState> GetAll()
        {
            return this.db.PaymentStates.ToList();
        }

        public PaymentState GetById(int id)
        {
            return this.db.PaymentStates.Find(id);
        }
    }
}
