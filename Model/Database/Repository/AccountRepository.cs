using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Database.Repository
{
    public class AccountRepository : Repository
    {
        public AccountRepository(PWDatabase db) : base(db)
        {
        }

        public void Create(Account entity)
        {
            this.db.Entry(entity).State = System.Data.Entity.EntityState.Added;
        }

        public IEnumerable<Account> GetAll()
        {
            return this.db.Accounts.ToList();
        }

        public Account GetById(int id)
        {
            return this.db.Accounts.Find(id);
        }

        public Account GetByLogin(string login)
        {
            return this.db.Accounts.FirstOrDefault(e => e.UserLogin == login);
        }

        public Account GetByName(string name)
        {
            return this.db.Accounts.FirstOrDefault(e => e.UserName == name);
        }

        public void Update(Account entity)
        {
            this.db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
