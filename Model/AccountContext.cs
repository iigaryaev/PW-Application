using Model.Database;
using Model.Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AccountContext
    {
        private readonly UnitOfWork database;

        private Account account;

        public AccountContext( PWDatabase db, Account account )
        {
            this.database = new UnitOfWork(db);
            this.account = account;
        }

        public AccountContext(PWDatabase db, int accountId)
        {
            this.database = new UnitOfWork(db);
            this.account = this.database.AccountRepository.GetById(accountId);
        }

        public void MakePayment(int correspondentId, int ammount)
        {
            this.database.PaymentRepository.Create(new Payment {
                SenderUserId = this.account.Id,
                CorrespondentUserId = correspondentId,
                Ammount = ammount });

            this.database.Save();
        }

        public IEnumerable<Payment> GetPaymentsHistory()
        {
            return this.database.PaymentRepository.GetAllForUser(this.account.Id);
        }

        public Account GetAccountState()
        {
            this.account = this.database.AccountRepository.GetById(this.account.Id);

            return this.account;
        }
    }
}
