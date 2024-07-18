﻿using ConsoleTests.Data;
using ConsoleTests.Data.Entities;
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
        static async Task Main(string[] args)
        {
            const string connectionStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Data\Databases\Students.DB;Integrated Security=True";

                //В реальном приложении было бы так:
            /*
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<StudentsDB>(opts => opts.UseSqlServer(connectionStr));
            var services = serviceCollection.BuildServiceProvider();

            using (var bd = services.GetRequiredService<StudentsDB>()) { } */

            using (var db = new StudentsDB(new DbContextOptionsBuilder<StudentsDB>().UseSqlServer(connectionStr).Options))
            {
                //await db.Database.EnsureCreatedAsync();
                await db.Database.MigrateAsync();

                var studentsCount = await db.Students.CountAsync();

                Console.WriteLine("Database students count: {0}", studentsCount);
            }

            //желательно НЕ использовать один экземпляр контекста на протяжении всей жизни приложения,
            //а каждый раз создавать новый контекст для маленьких запросов, иначе экземпляр контекста перегружается информацией и начинает тормозить
            using (var db = new StudentsDB(new DbContextOptionsBuilder<StudentsDB>().UseSqlServer(connectionStr).Options))
            {
                var k = 0;
                if (!await db.Students.AnyAsync())
                {
                    for (var i = 0; i < 10; i++)
                    {
                        var group = new Group
                        {
                            Name = $"Группа {i}",
                            Description = $"Описание группы {i}",
                            Students = new List<Student>()
                        };

                        for (var j = 0; j < 10; j++)
                        {
                            var student = new Student
                            {
                                Name = $"Студент {k}",
                                Surname = $"Фамилия {k}",
                                Patronymic = $"Отчество {k}",
                                Description = $"Описание {k}",
                            };
                            k++;
                            group.Students.Add(student);
                        }
                        await db.Groups.AddAsync(group);
                    }
                    await db.SaveChangesAsync();
                }
            }

            using (var db = new StudentsDB(new DbContextOptionsBuilder<StudentsDB>().UseSqlServer(connectionStr).Options))
            {
                var students = await db.Students
                    .Include(s => s.Group) //JOIN 
                    .Where(s => s.Group.Name == "Группа 5")
                    .ToArrayAsync(); //Создает sql-запрос

                foreach(var student in students)
                    Console.WriteLine("[{0}] {1} - {2}", student.Id, student.Name, student.Group.Name);
            }

                Console.WriteLine("Главный поток работу закончил!");
            Console.ReadLine();
        }
    }
}
