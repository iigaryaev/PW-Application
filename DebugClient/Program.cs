using Model;
using Model.Database;
using Model.Database.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new PWDatabase();
            var auth = new AuthContext(db);
            var account = auth.Authorize("iigaryaev@gmail.com", "qqqqqq");
            var ac = new AccountContext(db, account);

            var processor = PaymentsProcessor.Create(new PWDatabase(), 5);
            var t = Task.Run(() => processor.StartProcessing());

            //var stop = false;

            while(true)
            {
                ac.MakePayment(4, 10);

                var input = Console.ReadKey();
                if(input.KeyChar == 's')
                {
                    processor.Dispose();
                    t.Wait();
                    break;
                }
            }
            


            //t.Wait();

            //auth.Register("iliya.garyaev@beeper.ru", "iliya", "qqqqqq");
            //auth.Register("iigaryaev@gmail.com", "Black032", "qqqqqq");

            //var res = auth.Authorize("iigaryaev@gmail.com", "qqqqqq");
            //var res2 = auth.Authorize("iigaryaev@gmail.com", "sss");
            //var res3 = auth.Authorize("iigarsyaev@gmail.com", "qqqqqq");
        }
    }
}
