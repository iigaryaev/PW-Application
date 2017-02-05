using Model;
using Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PW_Application.Models
{
    public class PaymentProcessorRunner
    {
        private static readonly PaymentsProcessor processor = PaymentsProcessor.Create(new PWDatabase(), 5);
        private static Task t;
        public static void Run()
        {
            if(t == null || t.Status != TaskStatus.Running)
            {
                t = Task.Run(() => processor.StartProcessing());
            }
        }

        public static void Stop()
        {
            processor.Dispose();
            t.Wait();
        }
    }
}