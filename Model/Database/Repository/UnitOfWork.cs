using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Database.Repository
{
    public class UnitOfWork
    {
        protected readonly PWDatabase db;

        private PaymentRepository paymentRepository;

        private PaymentStateRepository paymentStateRepository;

        private AccountRepository accountRepository;
        public UnitOfWork(PWDatabase db)
        {
            this.db = db;
        }

        public PaymentRepository PaymentRepository
        {
            get
            {
                if (this.paymentRepository == null)
                {
                    this.paymentRepository = new PaymentRepository(this.db);
                }

                return this.paymentRepository;
            }
        }

        public AccountRepository AccountRepository
        {
            get
            {
                if (this.accountRepository == null)
                {
                    this.accountRepository = new AccountRepository(this.db);
                }

                return this.accountRepository;
            }
        }

        public PaymentStateRepository PaymentStateRepository
        {
            get
            {
                if (this.paymentStateRepository == null)
                {
                    this.paymentStateRepository = new PaymentStateRepository(this.db);
                }

                return this.paymentStateRepository;
            }
        }

        public void Save()
        {
            this.db.SaveChanges();
        }
    }
}
