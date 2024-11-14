using ConsoleTests.Data;
using ConsoleTests.Data.Entities;
using MailSender.lib.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var report = new StatisticReport(500);
            report.CreatePackage("Statistic.docx");
        }
    }
}
