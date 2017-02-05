using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Database
{
    public class PWDatabase : DbContext
    {
        public PWDatabase() : base("PWDatabase")
        {
            //System.Data.Entity.Database.SetInitializer<PWDatabase>(null);
            System.Data.Entity.Database.SetInitializer<PWDatabase>(new MigrateDatabaseToLatestVersion<PWDatabase, Configuration>());
            this.Configuration.AutoDetectChangesEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }


        public DbSet<Account> Accounts { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<PaymentState> PaymentStates { get; set; }
    }
}
