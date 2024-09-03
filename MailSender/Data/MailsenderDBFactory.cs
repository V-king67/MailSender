using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MailSender.Data
{
    internal class MailsenderDBFactory : IDesignTimeDbContextFactory<MailsenderDB>
    {
        public MailsenderDB CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MailsenderDB>();
            const string connectionStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Mailsender.DB;Integrated Security=True";
            optionsBuilder.UseSqlServer(connectionStr);

            return new MailsenderDB(optionsBuilder.Options);
        }
    }
}
