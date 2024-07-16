using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTests.Data
{
    public class StudentsDBFactory : IDesignTimeDbContextFactory<StudentsDB>
    {
        public StudentsDB CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentsDB>();
            const string connectionStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=D:\Proging\c#_3\MailSender\Tests\ConsoleTests\Data\Databases\Students.MDF;Integrated Security=True";
            optionsBuilder.UseSqlServer(connectionStr);

            return new StudentsDB(optionsBuilder.Options);
        }
    }
}
