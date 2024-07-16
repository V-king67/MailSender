using ConsoleTests.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTests.Data
{
    internal class StudentsDB : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }

        public StudentsDB(DbContextOptions<StudentsDB> options) : base(options) { }
    }
}
