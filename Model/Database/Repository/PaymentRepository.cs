using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Database.Repository
{
    public class PaymentRepository : Repository
    {
        public PaymentRepository(PWDatabase db) : base(db)
        {
        }

        public void Create(Payment entity)
        {
            this.db.Entry(entity).State = System.Data.Entity.EntityState.Added;
        }

        public IEnumerable<Payment> GetAllForUser(int userId)
        {
            return this.db.Payments
                .Include("SenderUser")
                .Include("CorrespondentUser")
                .Where(e => e.SenderUserId == userId || e.CorrespondentUserId == userId)
                .ToList();
        }

        public Payment GetById(int id)
        {
            return this.db.Payments.Find(id);
        }

        public Payment GetNextToProcess()
        {
            return this.db.Database.SqlQuery<Payment>(@"
set rowcount 1;

update p
	set StateId = 4
	output inserted.*
	from Payment p
	where p.Id = (select top 1 Id from Payment where StateId = 1 order by CreatedUtc);

set rowcount 0;
").FirstOrDefault();
        }

        public void Update(Payment entity)
        {
            this.db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
