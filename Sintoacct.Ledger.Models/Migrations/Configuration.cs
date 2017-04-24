namespace Sintoacct.Ledger.Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.IO;
    using System.Reflection;

    internal sealed class Configuration : DbMigrationsConfiguration<Sintoacct.Ledger.Models.LedgerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Sintoacct.Ledger.Models.LedgerContext context)
        {
            string sqlInit = "";
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Sintoacct.Ledger.Models.SQL.InitData.sql"))
            {
                using (StreamReader sr = new StreamReader(s,System.Text.Encoding.Unicode))
                {
                    sqlInit = sr.ReadToEnd();

                    sr.Close();
                }
            }

            //context.Database.ExecuteSqlCommand(sqlInit);
        }
    }
}
