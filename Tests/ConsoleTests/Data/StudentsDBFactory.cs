﻿using Microsoft.EntityFrameworkCore.Design;
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
            const string connectionStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Data\Databases\Students.DB;Integrated Security=True";
            optionsBuilder.UseSqlServer(connectionStr);

            return new StudentsDB(optionsBuilder.Options);
        }
    }
}