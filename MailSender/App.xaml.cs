﻿using MailSender.lib.Interfaces;
using MailSender.lib.Models;
using MailSender.lib.Service;
using MailSender.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace MailSender
{
    public partial class App : Application
    {
        static IHost __hosting;
        public static IHost Hosting => __hosting
            ??= Host.CreateDefaultBuilder(Environment.GetCommandLineArgs())
            .ConfigureServices(ConfigureServices)
            .Build();

        public static IServiceProvider Services => Hosting.Services;
        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();

#if DEBUG
            services.AddTransient<IMailService, DebugMailService>();
#else
            services.AddTransient<IMailService, SmtpMailService>();

#endif

            //var memoryStore = new DataStorageInMemory();
            //services.AddSingleton<IStorage<Server>>(memoryStore);
            //services.AddSingleton<IStorage<Sender>>(memoryStore);
            //services.AddSingleton<IStorage<Recipient>>(memoryStore);
            //services.AddSingleton<IStorage<Message>>(memoryStore);

            const string dataFileName = "Data\\MailSenderStorage.xml";
            var fileStorage = new DataStorageInXmlFile(dataFileName);
            services.AddSingleton<IStorage<Server>>(fileStorage);
            services.AddSingleton<IStorage<Sender>>(fileStorage);
            services.AddSingleton<IStorage<Recipient>>(fileStorage);
            services.AddSingleton<IStorage<Message>>(fileStorage);


            //services.AddScoped<>();

            //using (var scope = Services.CreateScope())
            //{
            //    var mailService = scope.ServiceProvider.GetRequiredService<IMailService>();
            //    var sender = mailService.GetSender("smtp.mail.ru", 25, true, "Login", "Password");
            //    sender.Send("sender@mail.ru", "recipient@mail.ru", "Mail subject", "Mail text");
            //}
        }
    }
}
