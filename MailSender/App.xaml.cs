using MailSender.lib.Interfaces;
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
        static IHost _hosting;
        public static IHost Hosting => _hosting
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
            services.AddSingleton<IEncryptorService, Rfc2898Encryptor>();

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
