using Model.Database;
using Model.Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PaymentsProcessor : IDisposable
    {
        private static PaymentsProcessor Processor;

        public static PaymentsProcessor Create(PWDatabase db, int processingPause)
        {
            if(Processor != null)
            {
                throw new Exception("PaymentProcessor already exists");
            }

            Processor = new PaymentsProcessor(db, processingPause);

            return Processor;
        }

        private readonly UnitOfWork database;

        private bool processingStarted = false;

        private bool stopReuqired = false;

        private readonly int processingPause;

        public void StartProcessing()
        {
            if(this.processingStarted)
            {
                throw new Exception("Processing already started");
            }

            this.processingStarted = true;

            while(!this.stopReuqired)
            {
                var processed = this.ProcessNext();
                if(!processed)
                {
                    System.Threading.Thread.Sleep(new TimeSpan(0, 0, this.processingPause));
                }
            }

            this.processingStarted = false;
            this.stopReuqired = false;
        }

        private PaymentsProcessor(PWDatabase db, int processingPause)
        {
            this.database = new UnitOfWork(db);
            this.processingPause = processingPause;
        }

        private bool ProcessNext()
        {
            var nextPayment = this.database.PaymentRepository.GetNextToProcess();
            if(nextPayment != null)
            {
                
                var sender = this.database.AccountRepository.GetById(nextPayment.SenderUserId);
                var correspondent = this.database.AccountRepository.GetById(nextPayment.CorrespondentUserId);

                if(sender.Balance < nextPayment.Ammount)
                {
                    nextPayment.StateId = 3;
                    nextPayment.ProcessingComment = "Insufficient funds";

                    this.database.PaymentRepository.Update(nextPayment);
                    this.database.Save();
                }
                else
                {
                    nextPayment.StateId = 2;
                    sender.Balance -= nextPayment.Ammount;
                    correspondent.Balance += nextPayment.Ammount;

                    this.database.PaymentRepository.Update(nextPayment);
                    this.database.AccountRepository.Update(sender);
                    this.database.AccountRepository.Update(correspondent);
                    this.database.Save();
                }

                return true;
            }

            return false;
        }

        public void Dispose()
        {
            this.stopReuqired = true;

            while(this.processingStarted)
            {
                System.Threading.Thread.Sleep(new TimeSpan(0, 0, this.processingPause));
            }
        }
    }
}
