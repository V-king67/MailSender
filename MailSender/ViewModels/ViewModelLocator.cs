using Microsoft.Extensions.DependencyInjection;

namespace MailSender.ViewModels
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public RecipientEditWindowViewModel RecipientEditModel => App.Services.GetRequiredService<RecipientEditWindowViewModel>();
    }
}
