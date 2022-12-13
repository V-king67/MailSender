using MailSender.lib.Models.Base;
using System.ComponentModel;

namespace MailSender.lib.Models
{
    public class Sender : Person, IDataErrorInfo//, INotifyDataErrorInfo //Динамическая проверка, построенная на событии
    {
        string IDataErrorInfo.Error { get; } = null;

        string IDataErrorInfo.this[string PropertyName]
        {
            get
            {
                switch (PropertyName)
                {
                    default: return null;

                    case nameof(Name):
                        var name = Name;
                        if (name is null) return "Имя не может быть пустой строкой";
                        if (name.Length < 2) return "Имя не должно быть короче 2 символов";
                        if (name.Length > 20) return "Имя не может быть длиннее 20 символов";

                        return null;

                    case nameof(Address):
                        return null;
                }
            }
        }
    }
}
