namespace Sintoacct.Ledger.Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.IO;

    internal sealed class Configuration : DbMigrationsConfiguration<Sintoacct.Ledger.Models.LedgerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Sintoacct.Ledger.Models.LedgerContext context)
        {
            string sqlInit = "";
            using (StreamReader sr = File.OpenText(@"F:\Sintoacct\Sintoacct.Ledger.Models\SQL\InitData.sql"))
            {
                sqlInit = sr.ReadToEnd();

                sr.Close();
            }

            context.Database.ExecuteSqlCommand(sqlInit);
        }
    }
}
