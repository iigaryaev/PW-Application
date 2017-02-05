using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Database
{
    public class Configuration : DbMigrationsConfiguration<PWDatabase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;

        }

        protected override void Seed(PWDatabase db)
        {
            db.PaymentStates.AddOrUpdate(e => e.Id, new[] {
                new PaymentState { Id = 1, Name = "Requested" },
                new PaymentState { Id = 2, Name = "Approved" },
                new PaymentState { Id = 3, Name = "Rejected" } ,
                new PaymentState { Id = 4, Name = "InProcess" } });

            db.Database.ExecuteSqlCommand(@"if (object_id('DF_Payment_CreatedUtc') is null)
	alter table Payment add constraint DF_Payment_CreatedUtc  default(getutcdate()) for CreatedUtc");

            db.Database.ExecuteSqlCommand(@"if (object_id('UQ_Account_UserName') is null)
	alter table Account add constraint UQ_Account_UserName  unique(UserName)");

            db.Database.ExecuteSqlCommand(@"if (object_id('UQ_Account_UserLogin') is null)
	alter table Account add constraint UQ_Account_UserLogin  unique(UserLogin)");

            db.Database.ExecuteSqlCommand(@"if (object_id('DF_Account_CreatedUtc') is null)
	alter table Account add constraint DF_Account_CreatedUtc  default(getutcdate()) for CreatedUtc");

            db.Database.ExecuteSqlCommand(@"if (object_id('CK_Payment_Users') is null)
	alter table Payment add constraint CK_Payment_Users  check(SenderUserId != CorrespondentUserId)");

            db.Database.ExecuteSqlCommand(@"if (object_id('CK_Payment_Ammount') is null)
	alter table Payment add constraint CK_Payment_Ammount  check(Ammount > 0)");

            db.Database.ExecuteSqlCommand(@"if (object_id('CK_Account_Balance') is null)
	alter table Account add constraint CK_Account_Balance  check(Balance >= 0)");
        }
    }
}
